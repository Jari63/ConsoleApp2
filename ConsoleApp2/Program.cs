﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// This application uses the Azure IoT Hub service SDK for .NET
// For samples see: https://github.com/Azure/azure-iot-sdk-csharp/tree/master/iothub/service
using System;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
//using Microsoft.Rest;

namespace back_end_application
{
	class BackEndApplication
	{
		private static ServiceClient s_serviceClient;

		// Connection string for your IoT Hub
		// az iot hub show-connection-string --hub-name {your iot hub name} --policy-name service
		private readonly static string s_connectionString = "HostName=iot-jari.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=0tQ2SiEmVhBn31x9YKh3oaGloxCg/AVWyh257b4IWoM=";

		// Invoke the direct method on the device, passing the payload
		private static async Task InvokeMethod()
		{
			var methodInvocation = new CloudToDeviceMethod("SetTelemetryInterval") { ResponseTimeout = TimeSpan.FromSeconds(30) };
			methodInvocation.SetPayloadJson("10");

			// Invoke the direct method asynchronously and get the response from the simulated device.
			var response = await s_serviceClient.InvokeDeviceMethodAsync("myEdgeDevice", "IotEdgeModule1", methodInvocation);

			Console.WriteLine("Response status: {0}, payload:", response.Status);
			Console.WriteLine(response.GetPayloadAsJson());
		}

		private static void Main(string[] args)
		{
			Console.WriteLine("IoT Hub Quickstarts #2 - Back-end application.\n");

			// Create a ServiceClient to communicate with service-facing endpoint on your hub.
			s_serviceClient = ServiceClient.CreateFromConnectionString(s_connectionString);
			InvokeMethod().GetAwaiter().GetResult();
			Console.WriteLine("Press Enter to exit.");
			Console.ReadLine();
		}
	}
}
