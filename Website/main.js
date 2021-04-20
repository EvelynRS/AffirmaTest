function getExampleCar() {
    const xhr = new XMLHttpRequest();
    // localhost:44382 should match whatever port your api is running on
    const url='https://localhost:44382/Cars/3f7c5757-23f6-4160-8ccb-fc60a741af81';
    xhr.open('GET', url);
    xhr.send();

    xhr.onreadystatechange = (e) => {
        if (xhr.readyState == 4 && xhr.status == 200) {
            var car = JSON.parse(xhr.responseText);

            var carDetailContainer = document.getElementById('carDetailContainer');
            carDetailContainer.getElementsByClassName('make')[0].innerHTML = car.make;
            carDetailContainer.getElementsByClassName('model')[0].innerHTML = car.model;
            carDetailContainer.getElementsByClassName('year')[0].innerHTML = car.year;
            carDetailContainer.getElementsByClassName('bodyStyleName')[0].innerHTML = car.bodyStyleName;
            carDetailContainer.getElementsByClassName('transmissionTypeName')[0].innerHTML = car.transmissionTypeName;
            carDetailContainer.getElementsByClassName('horsepower')[0].innerHTML = car.horsepower;
            carDetailContainer.getElementsByClassName('price')[0].innerHTML = car.price;
        }
    }
}