﻿using Microsoft.AspNetCore.Mvc;

class AcceptedResultConverter :
    ResultConverter<AcceptedResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, AcceptedResult result)
    {
        writer.WriteProperty(result, result.Location, "Location");
        ObjectResultConverter.WriteObjectResult(writer, result);
    }
}