
# Component 1

PRE-PROCESSING
A. Street centerlines as line segments in layers with separate names - CAPITALIZE, alhpanumeric
B. Site boundary as closed curves : polyines, nurbs, etc

INPUT
A. Input type: Panel: 
    1. (hierarchy) layer names for streets separated by comma : streeta, streetb,...,streetn
    2. (associated with above) setback distances in order : a, b, ... ,n
B. Input type: crv
    1. select all site boundary to be addressed by the component
C. Input type: numeric slider
    1. max height
    2. min height
    3. Total FSR
    
INTERMEDIATE OUTPUT
A. Object of List of List of line segments organized (grouped into a list) based on layer names
B. Process site boundary with closed curve for next round
C. Associate street type index with setback distance index
        
PROCESSING & FINAL OUTPUT
A. Find which site is affected by street-type and respective setback
B. Offset the site boundary and generate the appropriate closed curve
C. Extrude site closed curve based on Fsr calculations

USAGE
    Mass operations on geometry to meet overall FSR requirements

@ Extension (Component 1):

PRE-PROCESSING
A. Each site is a joined geometry where the sides of the site are a single, independent entity.
   eg: Line segments for 3 sides and an arc for the third. 
   The process will explode the site boundary and treat the individual entities as capcable of receiving setbacks 

INPUT 
A. Input type: Crv
   Each side of the site is affected by a different street type and associated setback dimension.

 
