using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SolariPDV.GradientView
{
    public class NavigationGradient : NavigationPage
    {
        public NavigationGradient(Page root) : base(root)
        {
        }

        public NavigationGradient()
        {
        }

        public static readonly BindableProperty RightColorProperty = BindableProperty.Create(propertyName: nameof(RightColor),
               returnType: typeof(Color),
               declaringType: typeof(NavigationGradient),
               defaultValue: Color.Black);

        public static readonly BindableProperty LeftColorProperty = BindableProperty.Create(propertyName: nameof(LeftColor),
               returnType: typeof(Color),
               declaringType: typeof(NavigationGradient),
               defaultValue: Color.Black);

        public Color RightColor
        {
            get { return (Color)GetValue(RightColorProperty); }
            set { SetValue(RightColorProperty, value); }
        }

        public Color LeftColor
        {
            get { return (Color)GetValue(LeftColorProperty); }
            set { SetValue(LeftColorProperty, value); }
        }
    }
}
