﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entap.Basic.Forms;
using Entap.Basic.Launch.Guide;
using Entap.Basic.Launch.Splash;

namespace SHIRO.CO
{
    public class SplashPageUseCase : BasicSplashPageUseCase
    {
        public SplashPageUseCase()
        {
        }

        public override async Task LoadAsync()
        {
            await Task.Delay(3000);
            var contents = new List<GuideContent>()
            {
                new GuideContent { Title = "SHIRO.COへようこそ", Description = $"SHIRO.COアプリを使って{Environment.NewLine}より快適なお買い物をお楽しみください。", IsDescriptionCentering = true, Next = "次へ", Source = "image_guide01.png"},
                new GuideContent { Title = "SHIRO.お得なクーポンをもらおう", Description = "会員登録をすると、さらにお得なクーポンが配信されます。", Next = "次へ", Source = "image_guide02.png" },
                new GuideContent { Title = "ギャラリーに展示しよう", Description = "毎月、SHIROアプリ内で共有された写真の中からカフェギャラリーに展示される写真が選ばれます。", Next = "はじめる", Source = "image_guide03.png" },
            };
            await PageManager.Navigation.SetMainPage<GuidePage>(new GuidePageViewModel(contents));
        }
    }
}