# ResilientClient

By combining the following policies, this solution can be taken into consideration as an option to add resiliency and fault handling when calling third party services.

**Policy Breakdown**

* CircuitBreaker Policy: Breaks on an exception; should stop the calls for a duration untill the circuit is closed agian.
* Retry Policy: The function OnRetry can be add for further logging of the request being made through the retry process. 
* Fallback Policy; as explained in the investigation description this can have a flag set to on or off for the operator to decide if the execution is to continue further or not. When the flag is set to on the fallback operation can return back a proper response to continue with the registration/login. 

Policies can be made to not have an effect on selected http requests. Such as POST request or request that have a specific path.

By having a default policy, this can be further extended to have specific policies per external client. This can be especially usefull when using a fallback policy, as specific objects can be returned, per implentation. 

**Resilient Delegating Handler**

This is a delegating handler that will be executing the policies on every http call made.

**Usage**

Create an HttpClient by using an HttpClientFactory and using the delegating handler to execute the policies before every http call.

Similarly when using an Ioc container this can be achieved by using the delegating handler in the container itself.
