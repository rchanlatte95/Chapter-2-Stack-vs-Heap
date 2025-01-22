using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Data;
using System.Runtime.InteropServices;
using UnityEngine.PlayerLoop;
using System.Security.Cryptography;

namespace CHAPTER4
{
    public class Main : MonoBehaviour
    {
        public TextMeshProUGUI ResultText;

        public int NumIterations = 10000;
        public int ArraySize = 10000;

        private void Awake()
        {
            ResultText.text = "Ready\n";
        }

        private void Start()
        {
            ResultText.text = "Ready to run\n";
        }

        public void TestStackVsHeap()
        {
            int stackResult = 0;
            int heapResult = 0;
            TestStackVsHeapWithResult(out stackResult, out heapResult);
        }
        void TestStackVsHeapWithResult(out int stackResult, out int heapResult)
        {
            stackResult = 0;
            heapResult = 0;

            double heapTime = 0d;
            double stackTime = 0d;

            int ArraySize = 10000;

            int[] heapArray1 = new int[ArraySize];
            int[] heapArray2 = new int[ArraySize];
            int[] heapArray3 = new int[ArraySize];

            for (int i = 0; i < ArraySize; i++)
            {
                heapArray1[i] = (int)(UnityEngine.Random.value * 100);
                heapArray2[i] = (int)(UnityEngine.Random.value * 100);
            }

            Span<int> stackArray1 = stackalloc int[ArraySize];
            Span<int> stackArray2 = stackalloc int[ArraySize];
            Span<int> stackArray3 = stackalloc int[ArraySize];

            for (int i = 0; i < ArraySize; i++)
            {
                stackArray1[i] = (int)(UnityEngine.Random.value * 100);
                stackArray2[i] = (int)(UnityEngine.Random.value * 100);
                stackArray3[i] = 0;
            }

            for (int t = 0; t < NumIterations; t++)
            {
                double time = Time.realtimeSinceStartupAsDouble;
                for (int i = 0; i < ArraySize; i++)
                    heapArray3[i] = heapArray1[i] + heapArray2[i];
                heapTime += Time.realtimeSinceStartupAsDouble - time;

                time = Time.realtimeSinceStartupAsDouble;
                for (int i = 0; i < ArraySize; i++)
                    stackArray3[i] = stackArray1[i] + stackArray2[i];
                stackTime += Time.realtimeSinceStartupAsDouble - time;

                for (int i = 0; i < ArraySize; i++)
                {
                    stackResult += stackArray3[i];
                    heapResult += heapArray3[i];
                }
            }

            string s = "NumIterations " + NumIterations.ToString("N0") + "\n";
            s += "Heap Test " + heapTime.ToString("G4") + "\n";
            s += "Stack Test " + stackTime.ToString("G4") + "\n";
            s += "Stack is " + (heapTime / stackTime).ToString("G4") + "x faster \n";
            ResultText.text = s;
        }
    }

}