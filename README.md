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


1.  Unit tests should be written :
bit.ly/1XF0J6H


## Interessting links:

### In Regards to hashcode
* https://stackoverflow.com/questions/371328/why-is-it-important-to-override-gethashcode-when-equals-method-is-overridden
* https://stackoverflow.com/questions/638761/gethashcode-override-of-object-containing-generic-array/639098#639098

### In Regards to testing

* https://enterprisecraftsmanship.com/posts/tdd-best-practices/
* https://enterprisecraftsmanship.com/posts/integration-testing-or-how-to-sleep-well-at-nights/