class FlightDisplayForCustomer {
    constructor( id, airLineCompanyName, originCountry, destinationCountry, departureTime, landingTime, remainingTickets, status) {
        this.id = id
        this.airLineCompanyName = airLineCompanyName
        this.originCountry = originCountry
        this.destinationCountry = destinationCountry
        this.departureTime = departureTime
        this.landingTime = landingTime
        this.remainingTickets = remainingTickets
        if (status == undefined) {
            this.status = "Landing..."
        }

    }
}