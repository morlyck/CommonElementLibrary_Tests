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
    }
    #endregion
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
        throw new NotImplementedException();
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
}