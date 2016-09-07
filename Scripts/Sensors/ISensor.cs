﻿/**
* Copyright 2016 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using IBM.Watson.DeveloperCloud.Utilities;
using IBM.Watson.Self.Topics;
using System;

namespace IBM.Watson.Self.Sensors
{
    //! Interface for any object that should be a sensor
    public abstract class ISensor
    {
        #region Private Data
        string m_SensorId = Guid.NewGuid().ToString();
        #endregion

        #region ISensor interface
        public abstract string GetSensorName();
        public abstract string GetDataType();
        public abstract string GetBinaryType();
        public abstract bool OnStart();
        public abstract bool OnStop();
        public abstract void OnPause();
        public abstract void OnResume();
        #endregion
    
        #region Public Functions
        public string GetSensorId() { return m_SensorId; }
        public void SendData( ISensorData a_Data )
        {
            if (! SensorManager.Instance.IsRegistered( this ) )
                throw new WatsonException( "SendData() invoked on unregisted sensors." );

            TopicClient.Instance.Publish( "sensor-proxy-" + m_SensorId, a_Data.ToBinary() );
        }
        #endregion
    }
}
