using System;
using Xamarin.Forms;

namespace Phoneword
{
	public class MainPage : ContentPage
	{
		private readonly Entry _numberEntry;
		private Button _translateButton;
		private Button _callButton;
		private Button _callHistoryButton;

		private string _translatedNumber;

		public MainPage()
		{
			this.Padding = new Thickness(20,
			                             Device.OnPlatform(40, 20, 20), 
			                             20, 
			                             20);
			var layout = new StackLayout
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Vertical,
				Spacing = 15
			};

			var label = new Label
			{
				HorizontalTextAlignment = TextAlignment.Center,
				Text = "Enter a Phoneword:"
			};
			_numberEntry = new Entry { Text = "1-855-XAMARIN" };
			_translateButton = new Button { Text = "Translate" };
			_callButton = new Button
			{
				Text = "Call",
				IsEnabled = false
			};
			_callHistoryButton = new Button
			{
				Text = "Call History",
				IsEnabled = false
			};

			layout.Children.Add(label);
			layout.Children.Add(_numberEntry);
			layout.Children.Add(_translateButton);
			layout.Children.Add(_callButton);
			layout.Children.Add(_callHistoryButton);

			_translateButton.Clicked += OnTranslate;
			_callButton.Clicked += OnCall;
			_callHistoryButton.Clicked += OnCallHistory;

			Content = layout;
		}

		async void OnCall(object sender, EventArgs e)
		{
			var alertTitle = "Dial a number";
			var alertMessage = string.Format("Would you like to call {0}?", _translatedNumber);
			var wantToCall = await DisplayAlert(alertTitle, alertMessage, "Yes", "No");

			if (wantToCall)
			{
				var dialer = DependencyService.Get<IDialer>();

				if (dialer != null)
				{
					App.PhoneNumbers.Add(_translatedNumber);
					_callHistoryButton.IsEnabled = true;
					var successCall = await dialer.DialAsync(_translatedNumber);
				}
			}
		}

		void OnCallHistory(object sender, EventArgs e)
		{
			Navigation.PushAsync(new CallHistoryPage());
		}

		public void OnTranslate(object sender, EventArgs e)
		{
			var enteredNumber = _numberEntry.Text;
			_translatedNumber = Translator.ToNumber(enteredNumber);

			if (!string.IsNullOrEmpty(_translatedNumber))
			{
				_callButton.Text = "Call " + _translatedNumber;
				_callButton.IsEnabled = true;
			}
			else
			{
				_callButton.Text = "Call"; // TODO: Refactor... set Call only in 1 place
				_callButton.IsEnabled = false;
			}
		}
	}
}

