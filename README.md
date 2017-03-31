
# RepoSync

	The project is under development. Please be patient. Thank you! :) 
    
### What do we want?  
* Based on http://wiki.sensenet.com/Client_library
* Compatible with sn6.4+ and sn7+
* Sync contents between SN and Other System
* Sync contents between SN and SN 
* Based on Provider Model, we want some built-in providers:
	* SN provider: connect to the SenseNet ECM with Client Library
	* File provider: connect to the FileSystem (or Windows File share) 
	* Other providers in the future (we won't develop these, maybe in the future … :) but the community maybe develop them ;) ) 
	* SFTP provider: connect to the FTP or Secure FTP systems 
	* TFS provider
	* GIT provider
	* Dropbox provider
	* Google Drive provider 
  
* We will develop a RepoSync Library which can use some other Application: 
	* RepoSync CLI (Console Application )
	* RepoSync WPI (Asp.Net Web Application)
	* RepoSync PISA  (Powershell Integrated Smart Application)
	* SN 6  Application (In the future, not yet)
	* SN 7  Application (In the future, not yet)
	* Mobile  Application (In the future, not yet)
	* SN Task Management Executor Application (In the future, not yet)
	* WPF desktop application  (In the future, not yet)
### RepoSync Library 
This is a framework which able to Sync SenseNet Contents between SenseNet ECM to Other systems. Contains an interface definition for Provider implementations. It's a modular framework, the users can develop own providers to reach their goals and beat their enemies and gain world domination for fun. 

Fulfill the following requirements: 
* Use provider models ( IReposyncProvider) 
* Use logger library 
* Provide these actions: 
	* Sync
	* Compare
	* Rollback (In the future, not yet)

### Applications which use RepoSync frameworks
Entry points for use RepoSync Framework and can be use two RepoSync Provider for syncing contents between systems.  Should provide configurations and contextual data to RepoSync. 
	
### RepoSync Providers: 
These providers can be implemented the IRepoSyncProvider Interface.
    

