using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace NewXamarinPages.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			/// <summary>
			/// This causes the iOS-specific implementation of Xamarin.Forms 
			/// to be loaded in the application before the root view controller 
			/// is set by the call to the LoadApplication method.
			/// </summary>
			global::Xamarin.Forms.Forms.Init();

			LoadApplication(new App());

			var x = typeof(Xamarin.Forms.Themes.DarkThemeResources);
			x = typeof(Xamarin.Forms.Themes.LightThemeResources);
			x = typeof(Xamarin.Forms.Themes.iOS.UnderlineEffect);

			return base.FinishedLaunching(app, options);
		}
	}
}

