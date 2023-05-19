using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Collections.Generic;

namespace AppBancoDigital.Controls
{
	[Preserve(AllMembers = true)]
	public class CustomCarouselView : CarouselView
	{
		private bool isSwipedRight = false;
		private int currentItemIndex = 0;

		protected override void OnChildAdded(Element child)
		{
			base.OnChildAdded(child);

			if (child is ContentView contentView)
			{
				SwipeGestureRecognizer swipeRight = new SwipeGestureRecognizer();
				swipeRight.Direction = SwipeDirection.Right;
				swipeRight.Swiped += OnSwipedRight;
				contentView.GestureRecognizers.Add(swipeRight);

				SwipeGestureRecognizer swipeLeft = new SwipeGestureRecognizer();
				swipeLeft.Direction = SwipeDirection.Left;
				swipeLeft.Swiped += OnSwipedLeft;
				contentView.GestureRecognizers.Add(swipeLeft);
			}
		}

		private void OnSwipedRight(object sender, SwipedEventArgs e) => isSwipedRight = true;

		private void OnSwipedLeft(object sender, SwipedEventArgs e) => isSwipedRight = false;

		protected override void OnCurrentItemChanged(EventArgs args)
		{
			base.OnCurrentItemChanged(args);

			currentItemIndex = (isSwipedRight) ? ++currentItemIndex : --currentItemIndex;

			int itemCount = 0;

			if (ItemsSource is IList itemList)
			{
				itemCount = itemList.Count;
			}
			else if (ItemsSource is IEnumerable<object> enumerable)
			{
				itemCount = enumerable.Count();
			}

			if (currentItemIndex < 0)
				currentItemIndex = itemCount - 1;
			else if (currentItemIndex >= itemCount)
				currentItemIndex = 0;
			
			CurrentItem = GetItem(currentItemIndex);

			isSwipedRight = false;
		}

		private object GetItem(int index)
		{
			if (ItemsSource is IList itemsSource)
				return itemsSource[index];
			else if (ItemsSource is IEnumerable<object> enumerable)
				return enumerable.ElementAt(index);
			
			return null;
		}
	}
}