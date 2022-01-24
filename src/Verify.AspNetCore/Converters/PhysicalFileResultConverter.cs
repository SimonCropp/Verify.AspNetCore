﻿using Microsoft.AspNetCore.Mvc;

class PhysicalFileResultConverter :
    ResultConverter<PhysicalFileResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, PhysicalFileResult result)
    {
        FileResultConverter.WriteFileData(writer, result);
        writer.WriteProperty(result, result.FileName, "FileName");
    }
}