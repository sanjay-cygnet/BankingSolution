Micro service architecture with DDD.

Folder Structure
BuildingBlocks: 
	Building Block that can have common functionality independent of any application. Generally those don't depends on other project libraries

Service: 
	Contains actual service. It can be Web API and its class libraries or hosted/Background services

Solution Items: 
	Here apart from solution items you can find sql script to migrate database

test : 
	Contains Unit Test cases, service wise folders


BuildingBlock's Folders
-----------------------------------------------------------------------------------------------
EventBus : 
	Helps to connect RabbitMQ only. Basic code to publish and consume queues. Also defines some queues used by application like EmailPublisher Queue

Repository:
	Generic Repository. Also has common extension method to connect any with database either sql/pgsql. It defines Unit of work also. Base Entity has some common property.

Shared:
	Common library for all services in application. This could contain common functionality or class/models.  Enums/constants.


UnitTestExtensions:
	Used to define common extension method that could be used in all unit test cases in application







Services
--------------------------------------------------------------------------------------------------
Customer:
	Customer related apis.


Notification:
	Can be used to create notification related api. For example, dynamic template creating for email. As of now it contains mainly background service that is used to consume email queue.





Database
------------------------------------------------------------------------------------------------------
- you can use data migration to create database for this application
or
- you can find DatabaseInsertScripts script file inside solution items folder.