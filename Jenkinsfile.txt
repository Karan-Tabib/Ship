pipeline {
    agent any
    stages {
        stage('Build Web API') {
            steps {
                dir('ShipmanagementAPI') {
                    sh 'dotnet restore'
                    sh 'dotnet build -c Release'
                    sh 'dotnet test'
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
