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

using System.Collections.Generic;

namespace Quarrel.Controls.Markdown.Parse.Inlines
{
    /// <summary>
    /// Represents a span containing code, or other text that is to be displayed using a
    /// fixed-width font.
    /// </summary>
    internal class CodeInline : MarkdownInline, IInlineLeaf
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CodeInline"/> class.
        /// </summary>
        public CodeInline()
            : base(MarkdownInlineType.Code)
        {
        }

        /// <summary>
        /// Gets or sets the text to display as code.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Converts the object into it's textual representation.
        /// </summary>
        /// <returns> The textual representation of this object. </returns>
        public override string ToString()
        {
            if (Text == null)
            {
                return base.ToString();
            }

            return "`" + Text + "`";
        }

        /// <summary>
        /// Adds the chars that if found means we might have a match to the trip checker.
        /// </summary>
        /// <param name="tripCharHelpers">List of trips.</param>
        internal static void AddTripChars(List<Helpers.Common.InlineTripCharHelper> tripCharHelpers)
        {
            tripCharHelpers.Add(new Helpers.Common.InlineTripCharHelper() { FirstChar = '`', Method = Helpers.Common.InlineParseMethod.Code });
        }

        /// <summary>
        /// Attempts to parse an inline code span.
        /// </summary>
        /// <param name="markdown"> The markdown text. </param>
        /// <param name="start"> The location to start parsing. </param>
        /// <param name="maxEnd"> The location to stop parsing. </param>
        /// <returns> A parsed inline code span, or <c>null</c> if this is not an inline code span. </returns>
        internal static Helpers.Common.InlineParseResult Parse(string markdown, int start, int maxEnd)
        {
            // Check the first char.
            if (start == maxEnd || markdown[start] != '`')
            {
                return null;
            }

            // There is an alternate syntax that starts and ends with two backticks.
            // e.g. ``sdf`sdf`` would be "sdf`sdf".
            int innerStart = start + 1;
            int innerEnd, end;
            if (innerStart < maxEnd && markdown[innerStart] == '`')
            {
                // Alternate double back-tick syntax.
                innerStart++;

                // Find the end of the span.
                innerEnd = Helpers.Common.IndexOf(markdown, "``", innerStart, maxEnd);
                if (innerEnd == -1)
                {
                    return null;
                }

                end = innerEnd + 2;
            }
            else
            {
                // Standard single backtick syntax.

                // Find the end of the span.
                innerEnd = Helpers.Common.IndexOf(markdown, '`', innerStart, maxEnd);
                if (innerEnd == -1)
                {
                    return null;
                }

                end = innerEnd + 1;
            }

            // The span must contain at least one character.
            if (innerStart == innerEnd)
            {
                return null;
            }

            // We found something!
            var result = new CodeInline
            {
                Text = markdown.Substring(innerStart, innerEnd - innerStart).Trim(' ', '\t', '\r', '\n'),
            };
            return new Helpers.Common.InlineParseResult(result, start, end);
        }
    }
}
