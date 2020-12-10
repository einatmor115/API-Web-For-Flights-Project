class Flight {
    constructor(id, airlineid, origincountry, destcountry, landingtime, status) {
        this.id = id
        this.airlineid = airlineid
        this.origincountry = origincountry
        this.destcountry = destcountry
        this.landingtime = landingtime
        if (status == undefined) {
            this.status = "Landing..."
        }

    }
}