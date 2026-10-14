pipeline {
    agent any

    options {
        timestamps()
        timeout(time: 15, unit: 'MINUTES')
        buildDiscarder(logRotator(numToKeepStr: '20'))
    }

    // Сборка запускается webhook-ом от GitHub (плагин GitHub) и вручную кнопкой "Build Now"
    triggers {
        githubPush()
    }

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        DOTNET_NOLOGO = '1'
        SOLUTION = 'JenkinsDemo.slnx'
    }

    stages {
        stage('Restore') {
            steps {
                sh 'dotnet --info'
                sh 'dotnet restore $SOLUTION'
            }
        }

        // Линтер: проверка стиля кода по .editorconfig. Если код не отформатирован - сборка падает.
        stage('Lint') {
            steps {
                sh 'dotnet format $SOLUTION --verify-no-changes --no-restore'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build $SOLUTION -c Release --no-restore'
            }
        }

        stage('Test') {
            steps {
                sh '''
                    dotnet test $SOLUTION -c Release --no-build \
                        --logger "junit;LogFilePath=$WORKSPACE/TestResults/junit/results.xml" \
                        --collect:"XPlat Code Coverage" \
                        --results-directory "$WORKSPACE/TestResults/coverage"
                '''
            }
            post {
                always {
                    junit allowEmptyResults: true, testResults: 'TestResults/junit/*.xml'
                    archiveArtifacts artifacts: 'TestResults/coverage/**/coverage.cobertura.xml', allowEmptyArchive: true
                }
            }
        }

        stage('Publish') {
            steps {
                sh 'dotnet publish src/TextToolkit.Cli -c Release --no-build -o publish'
                archiveArtifacts artifacts: 'publish/**', fingerprint: true
            }
        }
    }

    post {
        success { echo 'Build SUCCESS' }
        failure { echo 'Build FAILED' }
        cleanup { cleanWs() }
    }
}
