﻿/* Yet Another Forum.NET
 * Copyright (C) 2006-2009 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web;
using YAF.Classes.Data;

namespace YAF.Classes.Utils
{
	/// <summary>
	/// Class provides helper functions related to the forum path and urls as well as forum version information.
	/// </summary>
	public static class YafForumInfo
	{
		/// <summary>
		/// The forum path (client-side).
		/// May not be the actual URL of the forum.
		/// </summary>
		static public string ForumRoot
		{
			get
			{
				return UrlBuilder.Path;
			}
		}

		/// <summary>
		/// The forum path (server-side).
		/// May not be the actual URL of the forum.
		/// </summary>
		static public string ForumFileRoot
		{
			get
			{
				return UrlBuilder.FileRoot;
			}
		}

		/// <summary>
		/// Complete external (client-side) URL of the forum. (e.g. http://myforum.com/forum/
		/// </summary>
		static public string ForumBaseUrl
		{
			get
			{
				return UrlBuilder.BaseUrl + UrlBuilder.Path;
			}
		}

		/// <summary>
		/// Full Url to the Root of the Forum
		/// </summary>
		static public string ForumURL
		{
			get
			{
				return YafBuildLink.GetLink( ForumPages.forum, true );
			}
		}

		///<summary>
		/// Returns true if the current site is a localhost.
		///</summary>
		static public bool IsLocal
		{
			get
			{
				string s = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
				return s != null && s.ToLower() == "localhost";
			}
		}

		/// <summary>
		/// Helper function that creates the the url of a resource.
		/// </summary>
		/// <param name="resourceName"></param>
		/// <returns></returns>
		static public string GetURLToResource(string resourceName)
		{
			return string.Format("{1}resources/{0}", resourceName, YafForumInfo.ForumRoot);
		}

		#region Version Information
		///<summary>
		/// Creates a string that is the YAF Application Version from a long value
		///</summary>
		///<param name="code">Value of Current Version</param>
		///<returns>Application Version String</returns>
		static public string AppVersionNameFromCode(long code)
		{
			string version;

			if ((code & 0xF0) > 0 || (code & 0x0F) == 1)
			{
				version = String.Format("{0}.{1}.{2}{3}", (code >> 24) & 0xFF, (code >> 16) & 0xFF, (code >> 8) & 0xFF, Convert.ToInt32(((code >> 4) & 0x0F)).ToString("00"));
			}
			else
			{
				version = String.Format("{0}.{1}.{2}", (code >> 24) & 0xFF, (code >> 16) & 0xFF, (code >> 8) & 0xFF);
			}

			if ((code & 0x0F) > 0)
			{
				if ((code & 0x0F) == 1)
				{
					// alpha release...
					version += " alpha";
				}
				else if ((code & 0x0F) == 2)
				{
					version += " beta";
				}
				else
				{
					// Add Release Candidate
					version += string.Format(" RC{0}", (code & 0x0F) - 2);
				}
			}

			return version;
		}

		/// <summary>
		/// YAF Current App Version string
		/// </summary>
		static public string AppVersionName
		{
			get
			{
				return AppVersionNameFromCode(AppVersionCode);
			}
		}

		/// <summary>
		/// YAF Current Database Version
		/// </summary>
		static public int AppVersion
		{
			get
			{
				return 35;
			}
		}

		/// <summary>
		/// YAF Current Application Version
		/// </summary>
		static public long AppVersionCode
		{
			get
			{
				return 0x01090412;
			}
		}

		/// <summary>
		/// YAF Current Build Date
		/// </summary>
		static public DateTime AppVersionDate
		{
			get
			{
				return new DateTime( 2009, 10, 4 );
			}
		}
		#endregion
	}
}
