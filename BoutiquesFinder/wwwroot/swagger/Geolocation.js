if ('geolocation' in navigator) {
    navigator.geolocation.getCurrentPosition(function (position) {
        console.log(position.coords.latitude)
        console.log(position.coords.longitude)
        return position
    }, function (error) {
        console.log(error)
    })
}
else {
    console.log('none')
}