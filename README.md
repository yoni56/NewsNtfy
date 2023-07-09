# NewsNtfy
###### A small utility I wrote to stay up to date with the latest headline article on news sites.
 The utility monitors the websites by sending a simple GET request to the homepage, parse the DOM, and send a push notification to the phone. The utility has cache memory, which compares the current headline with cached version in order to prevent sending duplicates.
 
# Adding support to a news website
In order to add support to a news website, you create a new class that must implement IJob which is a simple interface that has 1 method, that the Job scheduler executes everytime the interval elapsed. Despite that, you could optionally inherit from BaseJob in order to access the browser instance which renders JS inline, or instead use your own http client without the need to inherit from BaseJob.