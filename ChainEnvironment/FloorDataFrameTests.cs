using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

using Newtonsoft.Json;
using CommonElement;

namespace CommonElement.Tests
{
    #region(FloorDataFrameのシリアライズ/デシリアライズ)
    [TestClass()]
    public class FloorDataFrameTests
    {
        [TestMethod()]
        public void SerializeTest() {

            string variableName = "t";
            string value = "value";

            FloorDataFrame<string> floorDataFrame = new FloorDataFrame<string>();
            floorDataFrame.Variables.Add(variableName, value);

            string text = floorDataFrame.Serialize(GeneralSd.Instance);

            FloorDataFrame<string> floorDataFrameAfter = new FloorDataFrame<string>();
            floorDataFrameAfter.Deserialize(GeneralSd.Instance, text);

            Assert.AreEqual(value, floorDataFrameAfter.Variables[variableName]);
        }
        [TestMethod()]
        public void SerializeTest2() {

            string variableName = "t";
            int dataValue = 16;

            FloorDataFrame<CustomSerializeClass> floorDataFrame = new FloorDataFrame<CustomSerializeClass>();
            CustomSerializeClass custom = new CustomSerializeClass();
            custom.Data = dataValue;
            floorDataFrame.Variables.Add(variableName, custom);

            string text = floorDataFrame.Serialize(GeneralSd.Instance);

            FloorDataFrame<CustomSerializeClass> floorDataFrameAfter = new FloorDataFrame<CustomSerializeClass>();
            floorDataFrameAfter.Deserialize(GeneralSd.Instance, text);

            Assert.AreEqual(dataValue, floorDataFrameAfter.Variables[variableName].Data);
        }
        [TestMethod()]
        public void SerializeTest3() {

            string variableName = "t";
            string variableName2 = "et";
            int dataValue = 16;

            FloorDataFrame<ChainEnvironment> floorDataFrame = new FloorDataFrame<ChainEnvironment>();
            ChainEnvironment env = new ChainEnvironment();
            env.SetValue<int>(variableName2, dataValue);
            floorDataFrame.Variables.Add(variableName, env);

            string text = floorDataFrame.Serialize(GeneralSd.Instance);

            FloorDataFrame<ChainEnvironment> floorDataFrameAfter = new FloorDataFrame<ChainEnvironment>();
            floorDataFrameAfter.Deserialize(GeneralSd.Instance, text);

            Assert.AreEqual(dataValue, floorDataFrameAfter.Variables[variableName].GetValue<int>(variableName2));
        }
    }
    #endregion
}

public class CustomSerializeClassSdReady
{
    public int Data = 0;
}
public class CustomSerializeClass : ICustomSerialize
{
    public string Serialize(ISerializerAndDeserializer serializer) {
        CustomSerializeClassSdReady sdReady = new CustomSerializeClassSdReady();
        sdReady.Data = Data;

        return serializer.Serialize(sdReady);
    }
    public void Deserialize(ISerializerAndDeserializer deserializer, string text) {
        CustomSerializeClassSdReady sdReady = deserializer.Deserialize<CustomSerializeClassSdReady>(text);
        Data = sdReady.Data;
    }

    public int Data = 0;
}

public class GeneralSd : CommonElement.ISerializerAndDeserializer
{
    public static GeneralSd Instance { get; } = new GeneralSd();

    public static string SerializeStatic(object obj) {
        return JsonConvert.SerializeObject(obj);
    }
    public static typ DeserializeStatic<typ>(string jsonText) {
        return JsonConvert.DeserializeObject<typ>(jsonText);
    }

    public static object DeserializeStatic(Type typ, string jsonText) {
        return JsonConvert.DeserializeObject(jsonText, typ);
    }

    //
    public string Serialize(object obj) {
        return JsonConvert.SerializeObject(obj);
    }
    public typ Deserialize<typ>(string jsonText) {
        return JsonConvert.DeserializeObject<typ>(jsonText);
    }

    public object Deserialize(Type typ, string jsonText) {
        return JsonConvert.DeserializeObject(jsonText, typ);
    }

    public string SerializeCustomOrBasic(object obj) {
        throw new NotImplementedException();
    }

    public typ DeserializeCustomOrBasic<typ>(string jsonText) {
        throw new NotImplementedException();
    }

    public object DeserializeCustomOrBasic(Type typ, string jsonText) {
        throw new NotImplementedException();
    }

    public string Serialize_Indented(object obj) {
        throw new NotImplementedException();
    }
}