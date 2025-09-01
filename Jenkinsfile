pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Install Dependencies') {
            steps {
                dir('ShipManagement/ShipApp') {
                    sh 'npm install'
                }
            }
        }

        stage('Lint') {
            steps {
                dir('ShipManagement/ShipApp') {
                    sh 'npm run lint'
                }
            }
        }

        stage('Unit Tests') {
            steps {
                dir('ShipManagement/ShipApp') {
                    sh 'npm test'
                }
            }
            post {
                always {
                    junit '**/test-results/**/*.xml' // if your tests generate JUnit-style reports
                }
            }
        }

        stage('Build') {
            steps {
                dir('ShipManagement/ShipApp') {
                    sh 'npm run build'
                }
            }
        }

        stage('Deployment') {
            steps {
                dir('ShipManagement/ShipApp') {
                    // Example: copy build files to server or deploy using CLI
                    sh '''
                    echo "Deploying application..."
                    # Example: scp -r dist/ user@server:/var/www/app/
                    '''
                }
            }
        }
    }

    post {
        success {
            echo 'Pipeline completed successfully!'
        }
        failure {
            echo 'Pipeline failed!'
        }
    }
}
