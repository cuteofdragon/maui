using System.Maui.CustomAttributes;
using System.Maui.Internals;

#if UITEST
using Xamarin.UITest;
using NUnit.Framework;
using System.Maui.Core.UITests;
#endif

namespace System.Maui.Controls.Issues
{
#if UITEST 
	[NUnit.Framework.Category(UITestCategories.UwpIgnore)]
#endif
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Github, 2767, "ArgumentException: NaN not valid for height", PlatformAffected.All)]
	public class Issue2767 : TestContentPage
	{
		protected override void Init()
		{
			var grid = new Grid
			{
				RowDefinitions =
				{
					new RowDefinition { Height = new GridLength(0, GridUnitType.Star) },
					new RowDefinition { Height = new GridLength(60, GridUnitType.Star) },
				},
				ColumnDefinitions =
				{
					new ColumnDefinition { Width = new GridLength(0, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) },
				}
			};
			grid.AddChild(new Label { Text = "Collapsed" }, 0, 0);
			grid.AddChild(new Label { Text = "Collapsed" }, 0, 1);
			grid.AddChild(new Label { Text = "Collapsed" }, 1, 0);
			grid.AddChild(new Label { Text = "Label 1:1" }, 1, 1);
			
			Content = new Frame
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Content = grid
			};
		}

#if UITEST
		[Test]
		public void Issue2767Test()
		{
			RunningApp.WaitForElement("Label 1:1");
			Assert.IsEmpty(RunningApp.Query("Collapsed"));
		}
#endif
	}
}