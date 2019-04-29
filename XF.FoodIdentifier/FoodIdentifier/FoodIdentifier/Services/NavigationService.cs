using FoodIdentifier.Interfaces;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodIdentifier.Services
{
    public class NavigationService : INavigationService
    {
        internal static object NavigationPage { get; private set; }

        private INavigation _navigation;

        private Dictionary<string, Type> _pages = new Dictionary<string, Type>();
        private bool _alreadyNavigating;

        public string CurrentPageKey
        {
            get
            {
                var lastPage = _navigation.NavigationStack.Last();

                var value = _pages.FirstOrDefault(x => x.Value == lastPage.GetType());
                return value.Key;
            }
        }

        public void ClearBackstack()
        {
            if (_navigation == null)
            {
                throw new NullReferenceException("Please initialize the navigation service on the NavigationPage.");
            }

            if (_navigation.NavigationStack.Count > 1)
            {
                var existingPages = _navigation.NavigationStack.ToList();
                existingPages.Reverse();

                foreach (var page in existingPages.Skip(1))
                {
                    _navigation.RemovePage(page);
                }
            }
        }

        public async Task ClosePopupAsync()
        {
            try
            {
                if (_navigation == null)
                {
                    throw new NullReferenceException("Please initialize the navigation service on the NavigationPage.");
                }

                await _navigation.PopPopupAsync(true);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"NavigationService.cs | ClosePopupAsync | {e}");
            }
        }

        public void Configure(string key, Type type)
        {
            lock (_pages)
            {
                if (_pages.ContainsKey(key))
                {
                    _pages[key] = type;
                }
                else
                {
                    _pages.Add(key, type);
                }
            }
        }

        public async Task GoBackAsync()
        {
            if (_navigation == null)
            {
                throw new NullReferenceException("Please initialize the navigation service on the NavigationPage.");
            }

            await _navigation.PopAsync();
        }

        public void Initialize(object navigationPageInstance)
        {
            var navigationPage = navigationPageInstance as NavigationPage;
            _navigation = navigationPage?.Navigation;

            NavigationPage = navigationPage;
        }

        public async Task NavigateToAsync(string key)
        {
            await NavigateToAsync(key, null);
        }

        public async Task NavigateToAsync(string key, object parameter)
        {
            if (!_alreadyNavigating) // avoid double navigation, e.g. in case of Double Tap on MainPage
            {
                _alreadyNavigating = true;

                var type = _pages[key];

                Page page = null;

                if (parameter != null)
                {
                    page = Activator.CreateInstance(type, parameter) as Page;
                }
                else
                {
                    page = Activator.CreateInstance(type) as Page;
                }

                if (_navigation == null)
                {
                    throw new NullReferenceException("Please initialize the navigation service on the NavigationPage.");
                }

                await _navigation.PushAsync(page);

                _alreadyNavigating = false;
            }
        }

        public async Task OpenPopupAsync(string key)
        {
            await OpenPopupAsync(key, null);
        }

        public async Task OpenPopupAsync(string key, object parameter)
        {
            await Task.Yield();

            Device.BeginInvokeOnMainThread(async () =>
            {
                var type = _pages[key];

                PopupPage page = null;

                if (parameter != null)
                {
                    page = Activator.CreateInstance(type, parameter) as PopupPage;
                }
                else
                {
                    page = Activator.CreateInstance(type) as PopupPage;
                }

                if (_navigation == null)
                {
                    throw new NullReferenceException("Please initialize the navigation service on the NavigationPage.");
                }

                await _navigation.PushPopupAsync(page, true);
            });
        }
    }
}
