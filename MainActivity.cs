using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using AlertDialog = Android.App.AlertDialog;
using UrbanSlangSolver.Data;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;
using Stream = System.IO.Stream;

namespace UrbanSlangSolver
{
    [Activity(Label = "UrbanSlangSolver", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button checkButton;
        EditText editTextInput;
        TextView showResult;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            checkButton = FindViewById<Button>(Resource.Id.StartButton);
            editTextInput = FindViewById<EditText>(Resource.Id.editTextInput);
            showResult = FindViewById<TextView>(Resource.Id.ShowResult);


            checkButton.Click += delegate
            {
                GetJsonData();
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void GetJsonData()
        {
            try
            {
                var assembly = typeof(MainActivity).GetTypeInfo().Assembly;
                Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.slangWordsList.json");

                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    var myDeserializedClass = JsonConvert.DeserializeObject<List<MatchedWord>>(json);

                    if (editTextInput.Text.Equals(myDeserializedClass[0].SlangWord, StringComparison.OrdinalIgnoreCase))
                    {
                        showResult.Text = myDeserializedClass[0].Description;
                    }
                    else if (editTextInput.Text.Equals(myDeserializedClass[1].SlangWord, StringComparison.OrdinalIgnoreCase))
                    {
                        showResult.Text = myDeserializedClass[1].Description;
                    }
                    else if (editTextInput.Text.Equals(myDeserializedClass[2].SlangWord, StringComparison.OrdinalIgnoreCase))
                    {
                        showResult.Text = myDeserializedClass[2].Description;
                    }
                    else
                    {
                        showResult.Text = Constants.NoMatch;
                    }                
                }
            }
            catch (Exception ex)
            {
                AlertDialog.Builder alertDialog = new AlertDialog.Builder(this);
                alertDialog.SetTitle(Constants.Error);
                alertDialog.SetMessage(Constants.ErrorNotify + ex.Message);
                alertDialog.SetNeutralButton(Constants.Ok, delegate
                {
                    alertDialog.Dispose();
                });
                alertDialog.Show();
            }
        }
    }
}