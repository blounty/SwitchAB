using System;
using MonoTouch.UIKit;
using System.Drawing;
using System.Collections.Generic;

namespace SwitchAB.Samples.iOS
{
    public class MyViewController : UIViewController
    {
        UIButton button;
        int numClicks = 0;
        float buttonWidth = 200;
        float buttonHeight = 50;

        public MyViewController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.Frame = UIScreen.MainScreen.Bounds;
            View.BackgroundColor = UIColor.White;
            View.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;

            button = UIButton.FromType(UIButtonType.RoundedRect);

            button.Frame = new RectangleF(
                View.Frame.Width / 2 - buttonWidth / 2,
                View.Frame.Height / 2 - buttonHeight / 2,
                buttonWidth,
                buttonHeight);

            Switcher.Current.TestWithTarget("ClickButtonText", new List<Action>
            {               
                () => 
                {
                     button.SetTitle("Click me", UIControlState.Normal);
                },
                () => 
                {
                     button.SetTitle("Dont Click me", UIControlState.Normal);
                }

           
            });

            button.TouchUpInside += (object sender, EventArgs e) =>
            {
                Switcher.Current.TargetAchieved("ClickButtonText"); 
            };

            button.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleTopMargin |
                UIViewAutoresizing.FlexibleBottomMargin;

            View.AddSubview(button);
        }

    }
}

