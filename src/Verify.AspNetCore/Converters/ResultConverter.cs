﻿using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

abstract class ResultConverter<T> :
    WriteOnlyJsonConverter<T>
    where T : ActionResult
{
    public override void Write(VerifyJsonWriter writer, T result, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("ResultType");
        serializer.Serialize(writer, result.GetType().Name);
        InnerWrite(writer, result, serializer);
        writer.WriteEndObject();
    }

    protected abstract void InnerWrite(JsonWriter writer, T result, JsonSerializer serializer);
}