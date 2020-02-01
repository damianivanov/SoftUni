namespace SIS.HTTP
{
    public enum HttpResponseCode
    {
        Ok=200,
        MovedPermanently=301,
        Found=302,
        TemporaryRedirect =307, //Keeps the same response code as the request
        Unauthorized=401,
        Forbidden=403,
        NotFound=404,
        InternalServerError=500,
        NotImplemented=501,
    }
}