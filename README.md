# ddd-review-dot-net-core
This is project for me to review main DDD concepts and to practice .Net Core

### Highlights

1. Always start with the core domain
2. Don't include several bounded contexts upfront
3. Always look for hidden abstractions (Money class as an example)

4. We should not use .Net value objects for representing **ddd value objects** , this is mostly because **structs** do not suport inheritance and so you would need to implement equals and hash methods within all of your classes.

5. The most important parts to be covered with unit tests are the **Domain Classes** (the inner most layer of the onion architecture): 
![alt text][unit-tests-tip]

[unit-tests-tip]: README-REFs/unit-tests-start-tip.png 


5. 1.  Unit tests should be written when using code first approach:
    
        You should start writting your tests once you are done experimenting with your code over your domain classes and you are sure that you've achieved a good enough design on it.

        **Code-first** approach for design experiments and them you should move to **Test-first** approach after you are confident on the class modelling.

6. Three important distinctions between Entities and Value objects and Entities: 
    * Reference vs Structural Equality
    * Mutability vs Immutability
    * **Lifespan: Value Objects should belong to Entities**

7. You should aim into depositing most of the logic that you are able to into value objects instead of entities, specially due to their immutability aspects.

8. A good rule of thumb while managing repositories is to have **one repository per aggregate**.
 
![alt text][repositories-vs-aggregates]

[repositories-vs-aggregates]: README-REFs/repositories-vs-aggregates.png 

* Another important point worth noting about repositories, is that their public methods should work with aggregate roots only, and all of their working with other *internal* entities should be done behind the scenes, either manually in the repository or using ORM mapping capabilities.

* It is a good idea to comission all of the communications with the database to repositories, even if such communications bypass the ORM (using SQL directly or calling stored procedures manually).

9. Creting the database: 

    1. Docker command: 

        ```shell 
        docker run --name sql-server-ddd-review-dotnet-core -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=reviewddd@123' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-CU10-ubuntu 
        ```



    2. Table creation: 
        ```sql
        CREATE DATABASE DddInPractice;
        ```


    3. Table creation: 
        ```sql
        use DddInPractice;
        
        CREATE TABLE dbo.SnackMachine
        (
            SnackMachineID bigint PRIMARY KEY,
            OneCentCount INT NOT NULL DEFAULT 0,
            TenCentCount INT NOT NULL DEFAULT 0,
            QuarterCount INT NOT NULL DEFAULT 0,
            OneDollarCount INT NOT NULL DEFAULT 0,
            FiveDollarCount INT NOT NULL DEFAULT 0,
            TwentyDollarCount INT NOT NULL DEFAULT 0);

        CREATE TABLE dbo.Snack
        (
            SnackID BIGINT NOT NULL PRIMARY KEY,
            Name    VARCHAR(200) NOT NULL
        );


        CREATE TABLE dbo.Slot
        (
            SlotID         BIGINT NOT NULL PRIMARY KEY,
            Quantity       BIGINT DEFAULT 0 NOT NULL,
            Price          DECIMAL DEFAULT 0 NOT NULL,
            SnackID        BIGINT NOT NULL CONSTRAINT slot_snack_fk REFERENCES dbo.Snack,
            SnackMachineID BIGINT NOT NULL CONSTRAINT slot_snackmachine_fk REFERENCES dbo.SnackMachine,
            Position       INT NOT NULL
        );

        ```
    4. Adding the first register: 
        ```sql 
        INSERT INTO dbo.SnackMachine VALUES(1,1,1,1,1,1,1);
        ```
    5. Creating the Hi/Lo table 

        ```sql 
            CREATE TABLE dbo.Ids(EntityName VARCHAR(40) NOT NULL,
            NextHigh INT NOT NULL DEFAULT 1);
            ;
        ```


    6. Preparing seeding data:

        ```sql

            INSERT INTO dbo.Snack VALUES(1,'Chocolate');
            INSERT INTO dbo.Snack VALUES(2,'Soda');
            INSERT INTO dbo.Snack VALUES(3,'Gum');
            
        ```

        ```sql

            INSERT INTO dbo.Slot VALUES(1, 10, 3.00, 1, 1, 1);
            INSERT INTO dbo.Slot VALUES(2, 10, 2.00, 2, 1, 2);
            INSERT INTO dbo.Slot VALUES(3, 10, 1.00, 3, 1, 3);
            
        ```


    7. Creating the application inside the Hi/Lo table 

        ```sql 
            INSERT INTO dbo.Ids VALUES('SnackMachine', 1);
            INSERT INTO dbo.Ids VALUES('Slot', 1);
            INSERT INTO dbo.Ids VALUES('Snack', 1);
            
        ```


    8. Your connectionString will be: 

        ```@"Server=localhost;Database=DddInPractice;User Id=sa;Password=reviewddd@123;"```

        when running locally, on a container exchange *localhost* for the *db container name*

    9. IMPORTANT: 

        ADD THE USING STATEMENT FOR 
        ```CSHARP
        using System.Data.SqlClient;
        ```
        Over the SessionFactory.cs file, 
        and also as Nuget reference to yout project: 
        ```XML
            <PackageReference Include="System.Data.SqlClient" Version="4.8.2" />

        ```
        **OTHERWISE IT WON'T WORK FOR NHIBERNATE !!!!**


## Aggeregates modeling and rules

1. For this system we have two aggregatres: 
    * Snacks
    * SnackMachine

    They should work as retainers of a group of related entities

    Entities outside of the aggregate should only have access to the root aggregate

    ![alt text][root-aggregate-access]

    [root-aggregate-access]: README-REFs/aggregates-expected-relations.png 

    This goes to other layers of the application:


    ![alt text][root-aggregate-access2]

    [root-aggregate-access2]: README-REFs/aggregates-vs-application-services.png 

2. When you are defining the boundaries between aggregates you should ask yourself: *"Does this entity makes sense without that other entity? If the answer is yes, they should be the root of two separate aggregates."*

    * Entities inside of an aggregrate should be cohesive and dependent of each other 

    * Entities of separated aggregates should be **losely coupled** in the relationships among itselfs.

3. Aggregates should be persisted with a eventually consistent state, beware of creating too much big aggregates: 
    * Most aggregates consist of 1 or 2 entities
    * 3 entities per aggregate is usually a max
        * this rule do not apply to a value object 

    * **1 to many** relationships should be viewed as **1 to some** relationships, and if you have more than *30 members* on the many side, it is a sign that you should separate those on a separated aggregate, this is mainly related to the fact that it will be harder to work on performance issues with your domain modeled like that.





## Interessting links:

### In Regards to hashcode
* https://stackoverflow.com/questions/371328/why-is-it-important-to-override-gethashcode-when-equals-method-is-overridden
* https://stackoverflow.com/questions/638761/gethashcode-override-of-object-containing-generic-array/639098#639098

### In Regards to testing

* https://enterprisecraftsmanship.com/posts/tdd-best-practices/
* https://enterprisecraftsmanship.com/posts/integration-testing-or-how-to-sleep-well-at-nights/

### In Regards to Dependency Injection in .Net Core

* https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0


### In Regards to Aggregates boundaries

* https://enterprisecraftsmanship.com/posts/cohesion-coupling-difference/

* https://enterprisecraftsmanship.com/posts/email-uniqueness-as-aggregate-invariant/

https://enterprisecraftsmanship.com/posts/optimistic-locking-automatic-retry/
