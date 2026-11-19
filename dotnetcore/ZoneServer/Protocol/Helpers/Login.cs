using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using InfServer.Network;
using InfServer.Game;


namespace InfServer.Protocol
{	/// <summary>
	/// Provides a series of functions for easily serialization of packets
	/// </summary>
	public partial class Helpers
	{	///////////////////////////////////////////////////
		// Member Functions
		//////////////////////////////////////////////////
		/// <summary>
		/// Serializes a login response packet
		/// </summary>
		static public void Login_Response(Client c, SC_Login.Login_Result result)
		{	//Formulate our reply!
			SC_Login reply = new SC_Login();

			reply.result = result;
            reply.serverVersion = Helpers._serverVersion;
			reply.zoneConfig = _server._config["server/zoneConfig"].Value;
			reply.popupMessage = "";

			c.sendReliable(reply);
		}

		/// <summary>
		/// (2026-11-19) The zone's own welcome popup: server.xml &lt;loginPopup value="..."/&gt; ("|" = new line), put in
		/// front of the login message (the stand-alone notice / the database's message) on a successful login.
		/// </summary>
		static public string ZoneLoginPopup(string message)
		{
			string zone = null;
			try
			{
				if (_server._config.Exists("server/loginPopup"))
					zone = _server._config["server/loginPopup"].Value;
			}
			catch { zone = null; }
			if (string.IsNullOrWhiteSpace(zone))
				return message;
			zone = zone.Replace("|", "\r\n");
			return string.IsNullOrEmpty(message) ? zone : zone + "\r\n\r\n" + message;
		}

		/// <summary>
		/// Serializes a login response packet
		/// </summary>
		static public void Login_Response(Client c, SC_Login.Login_Result result, string popupMessage)
		{	//Formulate our reply!
			SC_Login reply = new SC_Login();

			reply.result = result;
            reply.serverVersion = Helpers._serverVersion;
			reply.zoneConfig = _server._config["server/zoneConfig"].Value;
			reply.popupMessage = popupMessage;

			c.sendReliable(reply);
		}
	}
}
