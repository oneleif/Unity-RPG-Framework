﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using RT.Properties;
using Sirenix.OdinInspector;

namespace RT {
	public class StatThresholdConditionNode : Node {
		[Input(ShowBackingValue.Never, ConnectionType.Override)] public int inputValue;
		[Output] public bool evaluation;
		public int inputDisplayStatValue;
		public int threshold;
		public int maxValue;

		protected override void Init() {
			base.Init();

			NodePort fromNode = GetInputPort("inputValue");
			if(fromNode != null)
			{
				InitializeNode();
			}
		}

		private void InitializeNode() {
			inputDisplayStatValue = GetInputValue<int>("inputValue");
			maxValue = inputDisplayStatValue;
		}

		public override void OnCreateConnection(NodePort from, NodePort to) {
			InitializeNode();
		}

		private void EvaluateCondition() {
			float q = (100f / (float)threshold);
			float m = ((float)maxValue / (float)inputDisplayStatValue);
			evaluation = (q >= m);
		}

		[Button]
		public void Condition() {
			var inputPort = GetInputPort("inputValue");
			if(inputPort.IsConnected)
			{
				inputDisplayStatValue = GetInputValue<int>("inputValue");
				EvaluateCondition();
			}
		}

		public override object GetValue(NodePort port) {
			Condition();
			return evaluation;
		}


	}
}