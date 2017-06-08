// Case.ParallelWalls
// clsAPI.cs
// mnelson-CASE
// 2017/03/19/8:47 PM

using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using Microsoft.VisualBasic.ApplicationServices;

namespace Case.ParallelWalls.API
{
  internal class ClsApi
  {
    #region Private Members

    /// <summary>
    ///   Full Qualifying Domain Name
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>
    private static string GetFullDomainName()
    {
      try
      {
        // Domain
        string m_domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;

        // Host
        string m_hostName = Dns.GetHostName();

        if (!m_hostName.Contains(m_domainName))
        {
          // add the domain name part, if the hostname does not already include the domain name
          m_hostName = Convert.ToString(m_hostName + Convert.ToString(".")) + m_domainName;
        }

        return m_hostName;
      }
      catch
      {
      }

      // Failure
      return "";
    }

    /// <summary>
    ///   Credential
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>
    private static NetworkCredential GetCred()
    {
      return new NetworkCredential("somclient", "f9d4269b718121954503fa58dcb3c18e4dc59c32");
    }

    /// <summary>
    ///   API Header
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>
    private static string GetHeader()
    {
      return "http://api.addins.case-apps.com/";
    }

    /// <summary>
    ///   Public IP Address
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>
    private static string GetPublicIp()
    {
      try
      {
        const string c_url = "http://checkip.dyndns.org";
        WebRequest req = WebRequest.Create(c_url);
        using (WebResponse resp = req.GetResponse())
        {
          StreamReader sr = new StreamReader(resp.GetResponseStream());
          string response = sr.ReadToEnd().Trim();
          string[] a = response.Split(':');
          string a2 = a[1].Substring(1);
          string[] a3 = a2.Split('<');
          string a4 = a3[0];
          return a4;
        }
      }
      catch
      {
      }

      // Failure
      return "UNKNOWN";
    }

    #endregion

    #region Public Members

    /// <summary>
    ///   Record
    /// </summary>
    /// <param name="useProxy"></param>
    /// <remarks></remarks>
    public static void RecordUsage(bool useProxy = false)
    {
      // Ignore My Calls
      if (Environment.MachineName.ToLower() == "case-platinum" & Environment.UserName.ToLower() == "d.rudder") return;
      if (Environment.MachineName.ToLower() == "masterdonpc" & Environment.UserName.ToLower() == "masterdon") return;

      try
      {
        // Assembly Data for Tracking
        AssemblyInfo m_a = new AssemblyInfo(Assembly.GetExecutingAssembly());

        string m_url = GetHeader() + "logusage?ip=" + GetPublicIp();
        m_url += "&mac=" + GetFullDomainName();
        m_url += "&netb=" + Environment.MachineName;
        m_url += "&appn=" + m_a.Title;
        m_url += "&appv=" + m_a.Version;
        m_url += "&appf=" + "report";
        m_url += "&user=" + Environment.UserName;
        m_url += "&mn=" + "";

        // The Request Call
        HttpWebRequest m_req = (HttpWebRequest) WebRequest.Create(m_url);
        m_req.Credentials = GetCred();
        m_req.Method = "POST";
        if (useProxy == false) m_req.Proxy = null;

        // Response String
        m_req.GetResponse();
      }
      catch
      {
        try
        {
          if (useProxy == false) RecordUsage(true);
        }
        catch
        {
        }
      }
    }

    #endregion
  }
}