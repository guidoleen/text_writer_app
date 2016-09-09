using System;
using System.IO;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.Res;
using Android.Webkit;
using Android.Net;
using Android.Util;
using Java.IO;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using Android.Content;

namespace AppTextWrite.Droid
{
	// ConfigurationChanges=Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize
	[Activity (Label = "Een handige TextWriter", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Button Click Show TXT file
			Button button = FindViewById<Button> (Resource.Id.myButton);
			TextView txtvieuw = FindViewById<TextView> (Resource.Id.TxtView);
			string StrRead = StringTxtReader().ToString();
			button.Click += delegate {
				txtvieuw.Text = StrRead;

			};

			// Toevoegen Text
			Button butttoevoeg = FindViewById<Button> (Resource.Id.TextToevoeg);
			EditText _edittxt = FindViewById<EditText> (Resource.Id.editText);
			butttoevoeg.Click += delegate {
				WriterWrite(_edittxt.Text.ToString());
				StartActivity(typeof(MainActivity));
			};

			// Butto Clik Webpage show when wifi available
			Button buttHtml = FindViewById<Button> (Resource.Id.buttweb);
			buttHtml.Click += (object sender, EventArgs Ee) => {

				if(netconnect() == true)
				{
					buttWebShow_Click(sender, Ee);
				}
				else
				{
					// SetContentView (Resource.Layout.Main);
					txtvieuw.TextSize = 20;
					txtvieuw.Text = "Helaas geen verbinding mogelijk";
					// Toast.MakeText(this, "Helaas geen verbinding mogelijk", ToastLength.Short);
				}
			};

			Button _buttdelete = FindViewById<Button> (Resource.Id.buttdelete);
			_buttdelete.Click += EmtyTextFile_Click;


		}

		///////////////////////////////////////////////////////////////////
		/////////////// Methods buiten Activity Oncreate() ///////////////
		void buttWebShow_Click(Object s, EventArgs e)
		{
			Intent _intent = new Intent(this, typeof(Webshow));
			this.StartActivity(_intent);
			this.OverridePendingTransition (Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
		}

		// Method StringReader Text File
		public string StrTxtFile;
		public string StringTxtReader()
		{
			// Filepath and name 
			var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var filename = Path.Combine(path, "textfile.txt");
	
			// Check if file exists
			if(System.IO.File.Exists("files/textfile.txt") == false)
			{
				using (StreamReader _StreamRead = new StreamReader (filename)) {
					if (_StreamRead != null) {
						StrTxtFile = _StreamRead.ReadToEnd ();
					}
				};
			}
			else
			{
				using (StreamReader _StreamRead = new StreamReader (Application.Context.Assets.Open("files/textfile.txt")))
				{
					if (_StreamRead != null) {
						StrTxtFile = _StreamRead.ReadToEnd ();
					}
				}

			}
			return StrTxtFile;
		}
			
		// Method Write the text to
		public void WriterWrite(string _strwriter)
		{
				
//			using (OutputStreamWriter outputStreamWriter = new OutputStreamWriter (Application.Context.OpenFileInput ("textfile.txt"))) {
//				outputStreamWriter.Write (_strwriter);
//				outputStreamWriter.Close ();
//			};

			// get a writable file path
			var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var filename = Path.Combine(path, "textfile.txt");

			// write the data to the writable path - now you can read and write it
			string StrFlag =  StringTxtReader();
			_strwriter = (StrFlag + " " + _strwriter);

			System.IO.File.WriteAllText(filename, _strwriter);
		}

		// Check if WifiConnect
		public bool netconnect()
		{
			bool _netconn;
			ConnectivityManager connectivityManager = (ConnectivityManager) GetSystemService(ConnectivityService);
			NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;

			if((activeConnection != null) && activeConnection.IsConnected)
			{
				// _netconn = "Wifi Connected.";
				_netconn = true;
			} 
			else
			{
				// _netconn = "Wifi disconnected.";
				// Toast.MakeText();
				_netconn = false;
			}
			return _netconn;
		}

		// Method Empty file txt
		public void EmtyTextFile_Click(Object s, EventArgs E)
		{
			var direct  = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var file = Path.Combine (direct, "textfile.txt");

				System.IO.File.WriteAllText (file, "");
		}
	}
}
