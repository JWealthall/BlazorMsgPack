<h1>Blazor Message Pack</h1>

# Summary

This is just an example program to show how Message Pack can be used instead of JSON for transmission to/from an API.

There are pros and cons to both serialization methods.
The main reason for using Message Pack would be to increase the speed of transfer and reduce the size of the data.
The main reason for using JSON would be for an API where if a change is made to the data structure both ends are likely to still process it.

# Advantages/Disadvantages

## JSON

### Advantages

- Human readable
- Missing data tolerant
- Extra data tolerant

### Disadvantages

- Human readable (it's potentially a security risk)
- Larger data
- Slower
- Some formats (e.g. dates) lost in translation (i.e. they're different at both ends)

## Message Pack

### Advantages

- Binary format
- Smaller data (compression options available)
- Faster
- What you send is what you receive

### Disadvantages

- Both ends must use same data structure

# Notes

The data read is the standard _Weather Forecast_ data with 10K rows.

The time taken is the full round trip to the server.  This includes, the request, the serialization on the server, the transportation and the deserialization in the client.

The size doesn't not take into account any compression which may have taken place in the transportation layer itself.

The program uses a copy of the .Net JSON methods where I have modified the _read_ part to allow me to get the number of bytes read before deserializing.

The _Long Test_ runs through 101 cycles of each type.  Note that the first run is ignored from the averages as that is always the outlier.

Open up the browser's development tools and take a look at the _network_ tab to see what data is returned to each type.
