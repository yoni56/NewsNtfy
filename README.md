# NewsNtfy
A small utility I wrote to stay up-to-date with the latest headline article on news sites.
The utility monitors the websites by sending a simple GET request to the homepage, parsing the DOM, and sending a push notification to the phone. The utility has cache memory, which compares the current headline "hash" with the cached version in order to prevent sending duplicates.
 
# Adding support to a news website
In order to add support to a news website, you create a new class that must implement IJob, which is a simple interface that has one method, that the Job scheduler executes every time the interval elapses. Despite that, you could optionally inherit from BaseJob in order to access the browser instance, which renders JS inline, or instead use your own http client without the need to inherit from BaseJob in case the website serves static HTML content and does not need to render the content with JS.
