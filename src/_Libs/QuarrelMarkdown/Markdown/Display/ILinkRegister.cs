﻿// ******************************************************************
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************
// Copyright (c) Quarrel. All rights reserved.

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace Quarrel.Controls.Markdown.Display
{
    /// <summary>
    /// An internal interface used to handle links in the markdown.
    /// </summary>
    internal interface ILinkRegister
    {
        /// <summary>
        /// Registers a new hyperLink.
        /// </summary>
        /// <param name="newHyperlink">The HyperLink.</param>
        /// <param name="linkUrl">The url the HyperLink navigates to.</param>
        void RegisterNewHyperLink(Hyperlink newHyperlink, string linkUrl);

        /// <summary>
        /// Registers a new <see cref="HyperlinkButton"/>.
        /// </summary>
        /// <param name="newHyperlink">The <see cref="HyperlinkButton"/>.</param>
        /// <param name="linkUrl">The url the <see cref="HyperlinkButton"/> navigates to.</param>
        void RegisterNewHyperLink(HyperlinkButton newHyperlink, string linkUrl);
    }
}
