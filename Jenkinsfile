pipeline {
    agent any
    stages {
        stage('Build Web API') {
            steps {
                dir('ShipmanagementAPI/ShipAPI') {
                     sh 'dotnet restore ShipAPI.csproj'
					 sh 'dotnet build ShipAPI.csproj -c Release'
                }
            }
        }
        stage('Build Angular') {
            steps {
                dir('ShipManagement/ShipApp') {
                    sh 'npm install'
                    sh 'ng build --configuration production'
                }
            }
        }
    }
}
