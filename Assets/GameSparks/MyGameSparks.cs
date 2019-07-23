#pragma warning disable 612,618
#pragma warning disable 0114
#pragma warning disable 0108

using System;
using System.Collections.Generic;
using GameSparks.Core;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;

//THIS FILE IS AUTO GENERATED, DO NOT MODIFY!!
//THIS FILE IS AUTO GENERATED, DO NOT MODIFY!!
//THIS FILE IS AUTO GENERATED, DO NOT MODIFY!!

namespace GameSparks.Api.Requests{
		public class LogEventRequest_BigLevelUp : GSTypedRequest<LogEventRequest_BigLevelUp, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_BigLevelUp() : base("LogEventRequest"){
			request.AddString("eventKey", "BigLevelUp");
		}
	}
	
	public class LogChallengeEventRequest_BigLevelUp : GSTypedRequest<LogChallengeEventRequest_BigLevelUp, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_BigLevelUp() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "BigLevelUp");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_BigLevelUp SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
	}
	
	public class LogEventRequest_GiveCoin : GSTypedRequest<LogEventRequest_GiveCoin, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_GiveCoin() : base("LogEventRequest"){
			request.AddString("eventKey", "GiveCoin");
		}
		public LogEventRequest_GiveCoin Set_amount( long value )
		{
			request.AddNumber("amount", value);
			return this;
		}			
	}
	
	public class LogChallengeEventRequest_GiveCoin : GSTypedRequest<LogChallengeEventRequest_GiveCoin, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_GiveCoin() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "GiveCoin");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_GiveCoin SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_GiveCoin Set_amount( long value )
		{
			request.AddNumber("amount", value);
			return this;
		}			
	}
	
	public class LogEventRequest_LoadPlayerData : GSTypedRequest<LogEventRequest_LoadPlayerData, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_LoadPlayerData() : base("LogEventRequest"){
			request.AddString("eventKey", "LoadPlayerData");
		}
	}
	
	public class LogChallengeEventRequest_LoadPlayerData : GSTypedRequest<LogChallengeEventRequest_LoadPlayerData, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_LoadPlayerData() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "LoadPlayerData");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_LoadPlayerData SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
	}
	
	public class LogEventRequest_SavePlayerData : GSTypedRequest<LogEventRequest_SavePlayerData, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_SavePlayerData() : base("LogEventRequest"){
			request.AddString("eventKey", "SavePlayerData");
		}
		
		public LogEventRequest_SavePlayerData Set_NAME( string value )
		{
			request.AddString("NAME", value);
			return this;
		}
		public LogEventRequest_SavePlayerData Set_LEVEL( long value )
		{
			request.AddNumber("LEVEL", value);
			return this;
		}			
		public LogEventRequest_SavePlayerData Set_XP( long value )
		{
			request.AddNumber("XP", value);
			return this;
		}			
		
		public LogEventRequest_SavePlayerData Set_SKINUNLOCKS( string value )
		{
			request.AddString("SKINUNLOCKS", value);
			return this;
		}
		
		public LogEventRequest_SavePlayerData Set_CHARUNLOCKS( string value )
		{
			request.AddString("CHARUNLOCKS", value);
			return this;
		}
		
		public LogEventRequest_SavePlayerData Set_TRAILUNLOCKS( string value )
		{
			request.AddString("TRAILUNLOCKS", value);
			return this;
		}
		public LogEventRequest_SavePlayerData Set_WINS( long value )
		{
			request.AddNumber("WINS", value);
			return this;
		}			
		public LogEventRequest_SavePlayerData Set_LOSSES( long value )
		{
			request.AddNumber("LOSSES", value);
			return this;
		}			
	}
	
	public class LogChallengeEventRequest_SavePlayerData : GSTypedRequest<LogChallengeEventRequest_SavePlayerData, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_SavePlayerData() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "SavePlayerData");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_SavePlayerData SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_SavePlayerData Set_NAME( string value )
		{
			request.AddString("NAME", value);
			return this;
		}
		public LogChallengeEventRequest_SavePlayerData Set_LEVEL( long value )
		{
			request.AddNumber("LEVEL", value);
			return this;
		}			
		public LogChallengeEventRequest_SavePlayerData Set_XP( long value )
		{
			request.AddNumber("XP", value);
			return this;
		}			
		public LogChallengeEventRequest_SavePlayerData Set_SKINUNLOCKS( string value )
		{
			request.AddString("SKINUNLOCKS", value);
			return this;
		}
		public LogChallengeEventRequest_SavePlayerData Set_CHARUNLOCKS( string value )
		{
			request.AddString("CHARUNLOCKS", value);
			return this;
		}
		public LogChallengeEventRequest_SavePlayerData Set_TRAILUNLOCKS( string value )
		{
			request.AddString("TRAILUNLOCKS", value);
			return this;
		}
		public LogChallengeEventRequest_SavePlayerData Set_WINS( long value )
		{
			request.AddNumber("WINS", value);
			return this;
		}			
		public LogChallengeEventRequest_SavePlayerData Set_LOSSES( long value )
		{
			request.AddNumber("LOSSES", value);
			return this;
		}			
	}
	
	public class LogEventRequest_SimpleLevelUp : GSTypedRequest<LogEventRequest_SimpleLevelUp, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_SimpleLevelUp() : base("LogEventRequest"){
			request.AddString("eventKey", "SimpleLevelUp");
		}
	}
	
	public class LogChallengeEventRequest_SimpleLevelUp : GSTypedRequest<LogChallengeEventRequest_SimpleLevelUp, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_SimpleLevelUp() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "SimpleLevelUp");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_SimpleLevelUp SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
	}
	
	public class LogEventRequest_SpendMoney : GSTypedRequest<LogEventRequest_SpendMoney, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_SpendMoney() : base("LogEventRequest"){
			request.AddString("eventKey", "SpendMoney");
		}
		public LogEventRequest_SpendMoney Set_amount( long value )
		{
			request.AddNumber("amount", value);
			return this;
		}			
	}
	
	public class LogChallengeEventRequest_SpendMoney : GSTypedRequest<LogChallengeEventRequest_SpendMoney, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_SpendMoney() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "SpendMoney");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_SpendMoney SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_SpendMoney Set_amount( long value )
		{
			request.AddNumber("amount", value);
			return this;
		}			
	}
	
}
	
	
	
namespace GameSparks.Api.Requests{
	
	public class LeaderboardDataRequest_global : GSTypedRequest<LeaderboardDataRequest_global,LeaderboardDataResponse_global>
	{
		public LeaderboardDataRequest_global() : base("LeaderboardDataRequest"){
			request.AddString("leaderboardShortCode", "global");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LeaderboardDataResponse_global (response);
		}		
		
		/// <summary>
		/// The challenge instance to get the leaderboard data for
		/// </summary>
		public LeaderboardDataRequest_global SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		/// <summary>
		/// The number of items to return in a page (default=50)
		/// </summary>
		public LeaderboardDataRequest_global SetEntryCount( long entryCount )
		{
			request.AddNumber("entryCount", entryCount);
			return this;
		}
		/// <summary>
		/// A friend id or an array of friend ids to use instead of the player's social friends
		/// </summary>
		public LeaderboardDataRequest_global SetFriendIds( List<string> friendIds )
		{
			request.AddStringList("friendIds", friendIds);
			return this;
		}
		/// <summary>
		/// Number of entries to include from head of the list
		/// </summary>
		public LeaderboardDataRequest_global SetIncludeFirst( long includeFirst )
		{
			request.AddNumber("includeFirst", includeFirst);
			return this;
		}
		/// <summary>
		/// Number of entries to include from tail of the list
		/// </summary>
		public LeaderboardDataRequest_global SetIncludeLast( long includeLast )
		{
			request.AddNumber("includeLast", includeLast);
			return this;
		}
		
		/// <summary>
		/// The offset into the set of leaderboards returned
		/// </summary>
		public LeaderboardDataRequest_global SetOffset( long offset )
		{
			request.AddNumber("offset", offset);
			return this;
		}
		/// <summary>
		/// If True returns a leaderboard of the player's social friends
		/// </summary>
		public LeaderboardDataRequest_global SetSocial( bool social )
		{
			request.AddBoolean("social", social);
			return this;
		}
		/// <summary>
		/// The IDs of the teams you are interested in
		/// </summary>
		public LeaderboardDataRequest_global SetTeamIds( List<string> teamIds )
		{
			request.AddStringList("teamIds", teamIds);
			return this;
		}
		/// <summary>
		/// The type of team you are interested in
		/// </summary>
		public LeaderboardDataRequest_global SetTeamTypes( List<string> teamTypes )
		{
			request.AddStringList("teamTypes", teamTypes);
			return this;
		}
		
	}

	public class AroundMeLeaderboardRequest_global : GSTypedRequest<AroundMeLeaderboardRequest_global,AroundMeLeaderboardResponse_global>
	{
		public AroundMeLeaderboardRequest_global() : base("AroundMeLeaderboardRequest"){
			request.AddString("leaderboardShortCode", "global");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new AroundMeLeaderboardResponse_global (response);
		}		
		
		/// <summary>
		/// The number of items to return in a page (default=50)
		/// </summary>
		public AroundMeLeaderboardRequest_global SetEntryCount( long entryCount )
		{
			request.AddNumber("entryCount", entryCount);
			return this;
		}
		/// <summary>
		/// A friend id or an array of friend ids to use instead of the player's social friends
		/// </summary>
		public AroundMeLeaderboardRequest_global SetFriendIds( List<string> friendIds )
		{
			request.AddStringList("friendIds", friendIds);
			return this;
		}
		/// <summary>
		/// Number of entries to include from head of the list
		/// </summary>
		public AroundMeLeaderboardRequest_global SetIncludeFirst( long includeFirst )
		{
			request.AddNumber("includeFirst", includeFirst);
			return this;
		}
		/// <summary>
		/// Number of entries to include from tail of the list
		/// </summary>
		public AroundMeLeaderboardRequest_global SetIncludeLast( long includeLast )
		{
			request.AddNumber("includeLast", includeLast);
			return this;
		}
		
		/// <summary>
		/// If True returns a leaderboard of the player's social friends
		/// </summary>
		public AroundMeLeaderboardRequest_global SetSocial( bool social )
		{
			request.AddBoolean("social", social);
			return this;
		}
		/// <summary>
		/// The IDs of the teams you are interested in
		/// </summary>
		public AroundMeLeaderboardRequest_global SetTeamIds( List<string> teamIds )
		{
			request.AddStringList("teamIds", teamIds);
			return this;
		}
		/// <summary>
		/// The type of team you are interested in
		/// </summary>
		public AroundMeLeaderboardRequest_global SetTeamTypes( List<string> teamTypes )
		{
			request.AddStringList("teamTypes", teamTypes);
			return this;
		}
	}
}

namespace GameSparks.Api.Responses{
	
	public class _LeaderboardEntry_global : LeaderboardDataResponse._LeaderboardData{
		public _LeaderboardEntry_global(GSData data) : base(data){}
		public long? SUM_XP{
			get{return response.GetNumber("SUM-XP");}
		}
	}
	
	public class LeaderboardDataResponse_global : LeaderboardDataResponse
	{
		public LeaderboardDataResponse_global(GSData data) : base(data){}
		
		public GSEnumerable<_LeaderboardEntry_global> Data_global{
			get{return new GSEnumerable<_LeaderboardEntry_global>(response.GetObjectList("data"), (data) => { return new _LeaderboardEntry_global(data);});}
		}
		
		public GSEnumerable<_LeaderboardEntry_global> First_global{
			get{return new GSEnumerable<_LeaderboardEntry_global>(response.GetObjectList("first"), (data) => { return new _LeaderboardEntry_global(data);});}
		}
		
		public GSEnumerable<_LeaderboardEntry_global> Last_global{
			get{return new GSEnumerable<_LeaderboardEntry_global>(response.GetObjectList("last"), (data) => { return new _LeaderboardEntry_global(data);});}
		}
	}
	
	public class AroundMeLeaderboardResponse_global : AroundMeLeaderboardResponse
	{
		public AroundMeLeaderboardResponse_global(GSData data) : base(data){}
		
		public GSEnumerable<_LeaderboardEntry_global> Data_global{
			get{return new GSEnumerable<_LeaderboardEntry_global>(response.GetObjectList("data"), (data) => { return new _LeaderboardEntry_global(data);});}
		}
		
		public GSEnumerable<_LeaderboardEntry_global> First_global{
			get{return new GSEnumerable<_LeaderboardEntry_global>(response.GetObjectList("first"), (data) => { return new _LeaderboardEntry_global(data);});}
		}
		
		public GSEnumerable<_LeaderboardEntry_global> Last_global{
			get{return new GSEnumerable<_LeaderboardEntry_global>(response.GetObjectList("last"), (data) => { return new _LeaderboardEntry_global(data);});}
		}
	}
}	

namespace GameSparks.Api.Messages {


}
