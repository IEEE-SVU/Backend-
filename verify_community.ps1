$baseUrl = "http://localhost:5242/api/communities"

# 1. Create Community
Write-Output "Creating Community..."
$body = @{
    Name = "Test Community " + (Get-Date).ToString("yyyyMMddHHmmss")
    Description = "Test Desc"
    Achievments = @("Achievement 1", "Achievement 2")
    MainTasks = @("Task 1", "Task 2")
    IsJoiningOpen = $true
} | ConvertTo-Json

try {
    $response = Invoke-RestMethod -Uri $baseUrl -Method Post -Body $body -ContentType "application/json"
    Write-Output "Create Response: $($response | ConvertTo-Json -Depth 5)"
    $id = $response.data
} catch {
    Write-Error "Failed to create community: $_"
    exit 1
}

# 2. Get Community
Write-Output "Getting Community $id..."
try {
    $getResponse = Invoke-RestMethod -Uri "$baseUrl/$id" -Method Get
    Write-Output "Get Response: $($getResponse | ConvertTo-Json -Depth 5)"
    
    if ($getResponse.data.achievements.Count -eq 2 -and $getResponse.data.mainTasks.Count -eq 2) {
        Write-Output "SUCCESS: Achievements and Main Tasks retrieved correctly."
    } else {
        Write-Error "FAILURE: Data missing or incorrect count."
    }
} catch {
    Write-Error "Failed to get community: $_"
}

# 3. Update Community
Write-Output "Updating Community $id..."
$updateBody = @{
    Id = $id
    Name = "Updated Name"
    Description = "Updated Desc"
    Achievments = @("Achievement 1", "Achievement 3") # Removed 2, added 3
    MainTasks = @("Task 1") # Removed 2
    IsJoiningOpen = $true
} | ConvertTo-Json

try {
    Invoke-RestMethod -Uri $baseUrl -Method Put -Body $updateBody -ContentType "application/json"
    
    $getUpdated = Invoke-RestMethod -Uri "$baseUrl/$id" -Method Get
    Write-Output "Get Updated Response: $($getUpdated | ConvertTo-Json -Depth 5)"

    $achievements = $getUpdated.data.achievements
    $tasks = $getUpdated.data.mainTasks

    if ($achievements -contains "Achievement 3" -and $achievements -notcontains "Achievement 2" -and $tasks.Count -eq 1) {
         Write-Output "SUCCESS: Update reflected correctly."
    } else {
         Write-Output "ACTUAL DATA: Achievements: $($achievements -join ', '), Tasks: $($tasks -join ', ')"
         Write-Error "FAILURE: Update verification failed."
    }

} catch {
    Write-Error "Failed to update community: $_"
}
