﻿using Microsoft.AspNetCore.Mvc;

abstract class ResultConverter<T> :
    WriteOnlyJsonConverter<T>
    where T : ActionResult
{
    public override void Write(VerifyJsonWriter writer, T result)
    {
        writer.WriteStartObject();
        writer.WriteProperty(result,result.GetType().Name,"ResultType");
        InnerWrite(writer, result);
        writer.WriteEndObject();
    }

    protected abstract void InnerWrite(VerifyJsonWriter writer, T result);
}