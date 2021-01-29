using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Entap.Basic.Forms
{
    public class PageNavigation : IPageNavigation
    {
        public PageNavigation()
        {
            Init();
        }

        void Init()
        {
            // Androidデバイスの戻るボタンを検出するため、App.Current.ModalPoppedを購読
            Core.Application.ModalPopped += OnModalPopped;
        }

        void OnModalPopped(object sender, ModalPoppedEventArgs e)
        {
            if (IsNavigationPage(e.Modal))
                RemoveNavigationStack(e.Modal.Navigation);
            else
                OnPagePopped(e.Modal);

            var currentPage = GetCurrentPage();
            GetViewModelBase(currentPage)?.OnEntry();
            if (currentPage is TabbedPage tabbedPage)
                GetViewModelBase(tabbedPage.CurrentPage)?.OnEntry();
        }

        public virtual Page CreateNavigationPage(Page page)
        {
            try
            {
                var navigationPage = new NavigationPage(page);
                navigationPage.Behaviors.Add(new NavigationBehavior());
                return navigationPage;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("CreateNavigationPage : " + ex.Message);
                return null;
            }
        }

        public virtual Page CreateClosableNavigationPage(Page page)
        {
            var navigationPage = CreateNavigationPage(page);
            page.ToolbarItems.Add(new ToolbarItem()
            {
                Text = "閉じる",
                Command = new ProcessCommand(async (obj) =>
                {
                    await PageManager.Navigation.PopModalAsync(true);
                })
            });
            return navigationPage;
        }

        #region IPageNavigation

        #region CurrentPage制御
        /// <summary>
        /// ページ遷移順を管理するため、NavigationStack, ModalStack共通のStackを保有
        /// </summary>
        List<Guid> pageIdStack = new List<Guid>();

        public Page GetCurrentPage()
        {
            var lastItem = pageIdStack.LastOrDefault();
            // NavigationStackからページ取得
            var lastNavigationPage = currentNavigation.NavigationStack?.Where((arg) => arg.Id == lastItem).LastOrDefault();
            if (lastNavigationPage != null)
                return GetCurrentPage(lastNavigationPage);

            var lastModalPage = currentNavigation.ModalStack?.Where((arg) => arg.Id == lastItem).LastOrDefault();
            return GetCurrentPage(lastModalPage);
        }
        Page GetCurrentPage(Page page)
        {
            if (IsNavigationPage(page))
            {
                var navigationPage = (NavigationPage)page;
                return navigationPage.CurrentPage;
            }
            else if (page is TabbedPage tabbedPage)
                return tabbedPage.CurrentPage;
            return page;
        }

        public PageViewModelBase GetViewModelBase(Page page, object viewModel = null)
        {
            if (viewModel == null)
            {
                if (page == null)
                    return null;

                if (page is NavigationPage navigationPage)
                {
                    viewModel = navigationPage.CurrentPage?.BindingContext;
                }
                else
                {
                    viewModel = page.BindingContext;
                }
            }

            return viewModel as PageViewModelBase;
        }

        /// <summary>
        /// モーダルナビゲーションをスタックする。
        /// </summary>
        List<INavigation> modalNavigationStack;

        /// <summary>
        /// モーダルページを保有するか取得する。
        /// </summary>
        /// <value><c>true</c>：モーダルページ有り</value>
        bool hasModalNavigation => (modalNavigationStack == null || modalNavigationStack.Count == 0) ? false : true;

        /// <summary>
        /// 現在のNavigationを取得する。
        /// </summary>
        /// <value>The current navigation.</value>
        INavigation currentNavigation => (hasModalNavigation) ? modalNavigationStack.Last() : Core.Application.MainPage.Navigation;

        void ClearNavigationModalStack()
        {
            modalNavigationStack?.AsParallel().AsOrdered().ToArray().ForEach(RemoveNavigationStack);
            modalNavigationStack = null;
        }

        void RemoveNavigationStack(INavigation navigation)
        {
            if (navigation == null)
                return;

            navigation.NavigationStack?.AsParallel().AsOrdered().ToArray().ForEach((obj) => OnPagePopped(obj));
            navigation.ModalStack?.AsParallel().AsOrdered().ToArray().ForEach((obj) => OnPagePopped(obj));

            modalNavigationStack?.Remove(navigation);
        }
        #endregion

        #region メインページ設定
        /// <summary>
        /// 指定されたページをメインページに設定
        /// </summary>
        /// <param name="viewModel">ViewModelをバインドしたい場合に指定</param>
        /// /// <typeparam name="T">ページタイプ</typeparam>
        public Task<Task> SetMainPage<T>(PageViewModelBase viewModel = null) where T : Page
        {
            return SetMainPage<T>(false, viewModel);
        }

        /// <summary>
        /// NavigationPageをメインページに設定
        /// </summary>
        /// <param name="viewModel">ViewModelをバインドしたい場合に指定</param>
        /// <typeparam name="T">ページタイプ</typeparam>
        public Task<Task> SetNavigationMainPage<T>(PageViewModelBase viewModel = null) where T : Page
        {
            return SetMainPage<T>(true, viewModel);
        }

        Task<Task> SetMainPage<T>(bool isNavigationPage, PageViewModelBase viewModel) where T : Page
        {
            var page = (isNavigationPage) ? CreateNavigationPage<T>(viewModel) : CreatePage<T>(viewModel);

            if (Core.Application.MainPage == null)
            {
                SetMainPageBase(page, viewModel);
                return Task.FromResult<Task>(null);
            }
            else
            {
                var completionSource = new TaskCompletionSource<Task>();
                try
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        SetMainPageBase(page, viewModel);
                        completionSource.SetResult(completionSource.Task);
                    });
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    completionSource.SetResult(completionSource.Task);
                }
                return completionSource.Task;
            }
        }
        void SetMainPageBase(Page page, PageViewModelBase viewModel)
        {
            RemoveMainPageNavigationStack();
            Core.Application.MainPage = page;

            var currentPage = GetCurrentPage(page);
            pageIdStack.Add(currentPage.Id);

            GetViewModelBase(page, viewModel)?.OnEntry();
            ClearNavigationModalStack();
        }

        void RemoveMainPageNavigationStack()
        {
            if (Core.Application.MainPage == null) return;
            RemoveNavigationStack(Core.Application.MainPage?.Navigation);
            OnPagePopped(Core.Application.MainPage);
        }
        #endregion

        #region Page遷移
        /// <summary>
        /// PagePush
        /// </summary>
        /// <param name="viewModel">ViewModelをバインドしたい場合に指定</param>
        /// <param name="animated"><c>true</c>:ページ遷移後に、アニメーションを有効にする</param>
        /// <param name="removePages"><c>false</c>Pushするページ以外を削除する</param>
        /// <typeparam name="T">ページタイプ</typeparam>
        public Task PushAsync<T>(PageViewModelBase viewModel = null, bool animated = true, bool removePages = false) where T : Page
        {
            var page = CreatePage<T>(viewModel);
            return PushAsync(page, viewModel, animated, removePages);
        }
        public Task PushAsync(Page page, PageViewModelBase viewModel, bool animated = true, bool removePages = false)
        {
            var completionSource = new TaskCompletionSource<Task>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                var oldPage = GetCurrentPage();
                await currentNavigation.PushAsync(page, animated);

                if (removePages)
                {
                    // スタックされているPushされたページ以外のすべてのページを削除する
                    var removingPages = currentNavigation.NavigationStack.Where(p => pageIdStack.Contains(p.Id)).ToList();
                    foreach (var rp in removingPages)
                    {
                        currentNavigation.RemovePage(rp);
                        pageIdStack.Remove(rp.Id);
                    }
                }

                pageIdStack.Add(page.Id);

                GetViewModelBase(page, viewModel)?.OnEntry();
                GetViewModelBase(oldPage, null)?.OnExit();

                completionSource.SetResult(completionSource.Task);
            });
            return completionSource.Task;
        }

        /// <summary>
        /// PopPage
        /// </summary>
        /// <param name="animated"><c>true</c>:ページ遷移後に、アニメーションを有効にする</param>
        public Task PopAsync(bool animated = true)
        {
            var completionSource = new TaskCompletionSource<Task>();
            Device.BeginInvokeOnMainThread(async () =>
            {
                await currentNavigation.PopAsync(animated);

                completionSource.SetResult(completionSource.Task);
            });
            return completionSource.Task;
        }

        /// <summary>
        /// PushModalPage
        /// </summary>
        /// <param name="viewModel">ViewModelをバインドしたい場合に指定</param>
        /// <param name="animated"><c>true</c>:ページ遷移後に、アニメーションを有効にする</param>
        /// <typeparam name="T">ページタイプ</typeparam>
        public Task PushModalAsync<T>(PageViewModelBase viewModel = null, bool animated = true) where T : Page
        {
            var page = CreatePage<T>(viewModel);
            return PushModalAsync(page, viewModel, animated);
        }

        public Task PushModalAsync(Page page, PageViewModelBase viewModel = null, bool animated = true)
        {
            var completionSource = new TaskCompletionSource<Task>();
            Device.BeginInvokeOnMainThread(async () =>
            {
                var oldPage = GetCurrentPage();
                await currentNavigation.PushModalAsync(page, animated);

                var currentPage = GetCurrentPage(page);
                pageIdStack.Add(currentPage.Id);

                GetViewModelBase(page, viewModel)?.OnEntry();
                GetViewModelBase(oldPage, null)?.OnExit();

                if (IsNavigationPage(page))
                {
                    if (modalNavigationStack == null)
                        modalNavigationStack = new List<INavigation>();

                    modalNavigationStack.Add(page.Navigation);
                }

                completionSource.SetResult(completionSource.Task);
            });
            return completionSource.Task;
        }

        /// <summary>
        /// NavigationPageをPushModalする
        /// </summary>
        /// <typeparam name="T">ページタイプ</typeparam>
        /// <param name="viewModel">ViewModelをバインドしたい場合に指定</param>
        /// <param name="animated"><c>true</c>:ページ遷移後に、アニメーションを有効にする</param>
        /// <param name="hasNavigationCloseButton"><c>true</c>:閉じるボタンを表示する</param>
        public Task PushNavigationModalAsync<T>(PageViewModelBase viewModel = null, bool animated = true, bool hasNavigationCloseButton = false) where T : Page
        {
            var page = CreatePage<T>(viewModel);
            return PushNavigationModalAsync(page, viewModel, animated, hasNavigationCloseButton);
        }

        /// <summary>
        /// NavigationPageをPushModalする
        /// </summary>
        /// <param name="page">ページ</param>
        /// <param name="viewModel">ViewModelをバインドしたい場合に指定</param>
        /// <param name="animated"><c>true</c>:ページ遷移後に、アニメーションを有効にする</param>
        /// <param name="hasNavigationCloseButton"><c>true</c>:閉じるボタンを表示する</param>
        public Task PushNavigationModalAsync(Page page, PageViewModelBase viewModel = null, bool animated = true, bool hasNavigationCloseButton = false)
        {
            var navigationPage = hasNavigationCloseButton ?
                CreateClosableNavigationPage(page) :
                CreateNavigationPage(page);
            return PushModalAsync(navigationPage, viewModel, animated);
        }

        /// <summary>
        /// PopModalPage
        /// </summary>
        /// <param name="animated"><c>true</c>:ページ遷移後に、アニメーションを有効にする</param>
        public Task PopModalAsync(bool animated = true)
        {
            var completionSource = new TaskCompletionSource<Task>();
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    // LastPage退避
                    var lastPage = currentNavigation.ModalStack.LastOrDefault();
                    await currentNavigation.PopModalAsync(animated);
                    if (IsNavigationPage(lastPage))
                        RemoveNavigationStack(lastPage.Navigation);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"PopModalAsync : {ex.Message} _ {GetCurrentPage().GetType()}");
                    throw ex;
                }

                completionSource.SetResult(completionSource.Task);
            });
            return completionSource.Task;
        }

        public void OnPagePopped(Page page, PageViewModelBase viewModelBase = null)
        {
            // pageIdStackから削除
            pageIdStack.Remove(page.Id);
            // OnExit実行
            GetViewModelBase(page, viewModelBase)?.OnExit();
            // OnDestroy実行
            GetViewModelBase(page, viewModelBase)?.OnDestroy();
        }

        /// <summary>
        /// PopToRootPage
        /// </summary>
        /// <param name="animated"><c>true</c>:ページ遷移後に、アニメーションを有効にする</param>
        public Task PopToRootAsync(bool animated = true)
        {
            var completionSource = new TaskCompletionSource<Task>();
            Device.BeginInvokeOnMainThread(async () =>
            {
                OnPopToRoot(currentNavigation);
                await currentNavigation.PopToRootAsync(animated);

                completionSource.SetResult(completionSource.Task);
            });
            return completionSource.Task;
        }
        public void OnPopToRoot(INavigation navigation)
        {
            if (navigation == null)
                return;

            if (navigation.NavigationStack?.Count <= 1)
                return;

            var firstItem = navigation.NavigationStack.First();
            navigation.NavigationStack?
                      .Where((arg) => arg != firstItem)
                      .AsParallel()
                      .AsOrdered()
                      .ForEach((obj) => OnPagePopped(obj));
        }

        /// <summary>
        /// 現在のページの直前に、指定したページを挿入する
        /// </summary>
        /// <typeparam name="T">挿入するページ</typeparam>
        /// <param name="insertPageViewModel">挿入するページのViewModel</param>
        public void InsertPageBefore<T>(PageViewModelBase insertPageViewModel) where T : Page
        {
            currentNavigation.InsertPageBefore(CreatePage<T>(insertPageViewModel), GetCurrentPage());
        }
        #endregion

        #endregion

        #region ページ生成
        Page CreateNavigationPage<T>(PageViewModelBase viewModel = null) where T : Page
        {
            var page = CreatePage<T>(viewModel);
            return CreateNavigationPage(page);
        }

        /// <summary>
        /// 指定されたページを生成する。
        /// </summary>
        /// <returns>生成したページを返す</returns>
        /// <param name="viewModel">ViewModelをバインドしたい場合に指定</param>
        /// <typeparam name="T">ページのタイプ</typeparam>
        Page CreatePage<T>(PageViewModelBase viewModel = null) where T : Page
        {
            try
            {
                // ページのインスタンスを生成
                var page = (Page)Activator.CreateInstance(typeof(T));

                // ViewModelの指定がある場合、BindingContextに設定する。
                if (viewModel != null)
                    page.BindingContext = viewModel;

                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 指定したページがNavigationPageかどうか判定する。
        /// </summary>
        /// <returns><c>true</c>NavigationPage, <c>false</c> otherwise.</returns>
        /// <param name="page">Page.</param>
        bool IsNavigationPage(Page page)
        {
            return (page?.GetType() == typeof(NavigationPage) ||
                    page?.GetType()?.BaseType == typeof(NavigationPage)) ?
                true : false;
        }
        #endregion
    }
}
