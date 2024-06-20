# Variables
$imageName = "talpa-wesharp"
$imageTag = "latest"
$repoOwner = "DarkPheonix29"
$repoName = "TALPA"
$ghImageName = "ghcr.io/${repoOwner}/${repoName}/${imageName}:${imageTag}"

# Retrieve GitHub Container Registry PAT and Username from environment variables
$ghcrUsername = $env:GH_USERNAME
$ghcrPat = $env:GHCR_PAT

# Check if environment variables are set
if (-not $ghcrUsername) {
    Write-Error "GitHub Container Registry username is not set in environment variables."
    exit 1
}
if (-not $ghcrPat) {
    Write-Error "GitHub Container Registry PAT is not set in environment variables."
    exit 1
}

# Convert both username and repository name to lowercase for Docker
$dockerRepoOwner = $repoOwner.ToLower()
$dockerRepoName = $repoName.ToLower()

# Login to GitHub Container Registry in a non-interactive way
docker login ghcr.io -u $ghcrUsername -p $ghcrPat

# Tag the Docker image
$ghImageNameLower = "ghcr.io/${dockerRepoOwner}/${dockerRepoName}/${imageName}:${imageTag}"
docker tag ${imageName}:${imageTag} $ghImageNameLower

# Push the Docker image to GitHub Container Registry
docker push $ghImageNameLower
