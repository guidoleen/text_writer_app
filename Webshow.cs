
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;

namespace AppTextWrite.Droid
{
	[Activity (Label = "Webshow")]			
	public class Webshow : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Webshow);

			WebView localWebView = FindViewById<WebView>(Resource.Id.webview1);
			localWebView.Settings.JavaScriptEnabled = true;
			localWebView.LoadUrl ("http://m.guidoleen.nl");

		}
	}
}

