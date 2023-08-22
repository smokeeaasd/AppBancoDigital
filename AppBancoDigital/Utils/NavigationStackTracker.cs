using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AppBancoDigital.Utils
{
    public static class NavigationStackTracker
    {
        public static Stack<Page> Pages = new Stack<Page>();

        public static void PopAllAndPushTo(this Page p, Page page)
        {
            
        }
    }
}
