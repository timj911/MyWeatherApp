// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MyWeatherApp.iOS
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        UIKit.UIButton Button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel dateLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel descriptionLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel locationLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel maxLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel minLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView weatherImage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (dateLabel != null) {
                dateLabel.Dispose ();
                dateLabel = null;
            }

            if (descriptionLabel != null) {
                descriptionLabel.Dispose ();
                descriptionLabel = null;
            }

            if (locationLabel != null) {
                locationLabel.Dispose ();
                locationLabel = null;
            }

            if (maxLabel != null) {
                maxLabel.Dispose ();
                maxLabel = null;
            }

            if (minLabel != null) {
                minLabel.Dispose ();
                minLabel = null;
            }

            if (weatherImage != null) {
                weatherImage.Dispose ();
                weatherImage = null;
            }
        }
    }
}