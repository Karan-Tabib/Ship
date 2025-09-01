pipeline {
    agent any
    stages {
        stage('Build Web API') {
            steps {
                dir('ShipmanagementAPI/ShipAPI') {
                     sh 'dotnet restore ShipmanagementAPI.csproj'
					 sh 'dotnet build ShipmanagementAPI.csproj -c Release'
                }
            }
        }
        stage('Build Angular') {
            steps {
                dir('Shipmanagement') {
                    sh 'npm install'
                    sh 'ng build --configuration production'
                }
            }
        }
    }
}
