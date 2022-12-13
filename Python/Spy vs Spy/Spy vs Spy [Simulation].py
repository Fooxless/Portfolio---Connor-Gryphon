#--------------QUT Assignment----------------------------------------#
#
#    Student no: n10776800
#    Student name: Connor Gryphon
#
#--------------------------------------------------------------------#


#-----Task Description-----------------------------------------------#
#
#  LAND GRAB
#
#  This assignment tests your skills at processing data stored in
#  lists, creating reusable code and following instructions to display
#  a complex visual image.  The incomplete Python program below is
#  missing a crucial function, "process_moves".  You are required to
#  complete this function so that when the program runs it fills
#  a grid with various rectangular icons, using data stored in a
#  list to determine which icons to place and where.  See the
#  instruction sheet accompanying this file for full details.
#
#--------------------------------------------------------------------#  



#-----Preamble-------------------------------------------------------#
#
# This section imports necessary functions and defines constant
# values used for creating the drawing canvas.  You should not change
# any of the code in this section.
#

# Import the functions needed to complete this assignment.  You
# should not need to use any other modules for your solution.  In
# particular, your solution must NOT rely on any non-standard Python
# modules that need to be downloaded and installed separately,
# because the markers will not have access to such modules.
from turtle import *
from math import *
from random import *

# Define constant values for setting up the drawing canvas
cell_width = 120 # pixels (default is 120)
cell_height = 90 # pixels (default is 90)
grid_size = 7 # width and height of the grid (default is 7)
x_margin = cell_width * 2.4 # pixels, the size of the margin left/right of the board
y_margin = cell_height // 2.1 # pixels, the size of the margin below/above the board
canvas_height = grid_size * cell_height + y_margin * 2
canvas_width = grid_size * cell_width + x_margin * 2

# Validity checks on grid size
assert cell_width >= 100, 'Cells must be at least 100 pixels wide'
assert cell_height >= 75, 'Cells must be at least 75 pixels high'
assert grid_size >= 5, 'Grid must be at least 5x5'
assert grid_size % 2 == 1, 'Grid size must be odd'
assert cell_width / cell_height >= 4 / 3, 'Cells must be much wider than high'

#
#--------------------------------------------------------------------#



#-----Functions for Creating the Drawing Canvas----------------------#
#
# The functions in this section are called by the main program to
# manage the drawing canvas for your image.  You may NOT change
# any of the code in this section.
#

# Set up the canvas and draw the background for the overall image
def create_drawing_canvas(show_instructions = True, # show Part B instructions
                          label_locations = True, # label axes and home coord
                          bg_colour = 'light grey', # background colour
                          line_colour = 'grey'): # line colour for grid
    
    # Set up the drawing canvas with enough space for the grid
    setup(canvas_width, canvas_height)
    bgcolor(bg_colour)

    # Draw as quickly as possible
    tracer(False)

    # Get ready to draw the grid
    penup()
    color(line_colour)
    width(2)

    # Determine the left-bottom coordinate of the grid
    left_edge = -(grid_size * cell_width) // 2 
    bottom_edge = -(grid_size * cell_height) // 2

    # Draw the horizontal grid lines
    setheading(0) # face east
    for line_no in range(0, grid_size + 1):
        penup()
        goto(left_edge, bottom_edge + line_no * cell_height)
        pendown()
        forward(grid_size * cell_width)
        
    # Draw the vertical grid lines
    setheading(90) # face north
    for line_no in range(0, grid_size + 1):
        penup()
        goto(left_edge + line_no * cell_width, bottom_edge)
        pendown()
        forward(grid_size * cell_height)

    # Optionally label the axes and centre point
    if label_locations:

        # Mark the centre of the board (coordinate [0, 0])
        penup()
        home()
        dot(30)
        pencolor(bg_colour)
        dot(20)
        pencolor(line_colour)
        dot(10)

        # Define the font and position for the axis labels
        small_font = ('Arial', (18 * cell_width) // 100, 'normal')
        y_offset = (32 * cell_height) // 100 # pixels

        # Draw each of the labels on the x axis
        penup()
        for x_label in range(0, grid_size):
            goto(left_edge + (x_label * cell_width) + (cell_width // 2), bottom_edge - y_offset)
            write(chr(x_label + ord('A')), align = 'center', font = small_font)

        # Draw each of the labels on the y axis
        penup()
        x_offset, y_offset = 7, 10 # pixels
        for y_label in range(0, grid_size):
            goto(left_edge - x_offset, bottom_edge + (y_label * cell_height) + (cell_height // 2) - y_offset)
            write(str(y_label + 1), align = 'right', font = small_font)

    # Optionally write the instructions
    if show_instructions:
        # Font for the instructions
        big_font = ('Arial', (24 * cell_width) // 100, 'normal')
        # Text to the right of the grid
        penup()
        goto((grid_size * cell_width) // 2 + 50, -cell_height // 3)
        write('This space\nreserved for\nPart B', align = 'left', font = big_font)
        
    # Reset everything ready for the student's solution
    pencolor('black')
    width(1)
    penup()
    home()
    tracer(True)


# End the program and release the drawing canvas to the operating
# system.  By default the cursor (turtle) is hidden when the
# program ends.  Call the function with False as the argument to
# prevent this.
def release_drawing_canvas(hide_cursor = True):
    # Ensure any drawing still in progress is displayed
    update()
    tracer(True)
    # Optionally hide the cursor
    if hide_cursor:
        hideturtle()
    # Release the drawing canvas
    done()
    
#
#--------------------------------------------------------------------#



#-----Test Data for Use During Code Development----------------------#
#
# The following data set makes no moves at all and can be used
# when developing the code to draw the competitors in their
# starting positions.
fixed_data_set_00 = []

# The following data sets each move one of the competitors
# several times but do not attempt to go outside the margins
# of the grid or overwrite previous moves
fixed_data_set_01 = [['Competitor A', 'Right'],
                     ['Competitor A', 'Down'],
                     ['Competitor A', 'Down'],
                     ['Competitor A', 'Left'],
                     ['Competitor A', 'Up']]
fixed_data_set_02 = [['Competitor B', 'Left'],
                     ['Competitor B', 'Left'],
                     ['Competitor B', 'Down'],
                     ['Competitor B', 'Down'],
                     ['Competitor B', 'Right'],
                     ['Competitor B', 'Up']]
fixed_data_set_03 = [['Competitor C', 'Up'],
                     ['Competitor C', 'Up'],
                     ['Competitor C', 'Right'],
                     ['Competitor C', 'Right'],
                     ['Competitor C', 'Down'],
                     ['Competitor C', 'Down'],
                     ['Competitor C', 'Left']]
fixed_data_set_04 = [['Competitor D', 'Left'],
                     ['Competitor D', 'Left'],
                     ['Competitor D', 'Up'],
                     ['Competitor D', 'Up'],
                     ['Competitor D', 'Right'],
                     ['Competitor D', 'Up'],
                     ['Competitor D', 'Right'],
                     ['Competitor D', 'Down']]

# The following data set moves all four competitors and
# will cause them all to go outside the grid unless such
# moves are prevented by your code
fixed_data_set_05 = [['Competitor C', 'Right'],
                     ['Competitor B', 'Up'],
                     ['Competitor D', 'Down'],
                     ['Competitor A', 'Left'],
                     ['Competitor C', 'Down'],
                     ['Competitor B', 'Down'],
                     ['Competitor D', 'Left'],
                     ['Competitor A', 'Up'],
                     ['Competitor C', 'Up'],
                     ['Competitor B', 'Right'],
                     ['Competitor D', 'Right'],
                     ['Competitor A', 'Down'],
                     ['Competitor C', 'Right'],
                     ['Competitor B', 'Down'],
                     ['Competitor D', 'Right'],
                     ['Competitor A', 'Right']]
#
#--------------------------------------------------------------------#



#-----Function for Assessing Your Solution---------------------------#
#
# The function in this section will be used to assess your solution.
# Do not change any of the code in this section.

# The following function creates a random data set as a list
# of moves.  Your program must work for any data set that
# can be returned by this function.  The results returned by
# calling "random_moves()" will be used as the argument to your
# "process_moves" function during marking.  For convenience during
# code development and marking this function also prints each move
# to the shell window.
#
# NB: As a matter of style your code should not print anything else
# to the shell.  Make sure any debugging calls to the "print"
# function are disabled before you submit your solution.
#
# The function makes no attempt to avoid moves that will go
# outside the grid.  It is your responsibility to detect and
# ignore such moves.
#
def random_moves(the_seed = None, max_rounds = 35):
    # Welcoming message
    print('\nWelcome to Land Grab!')
    print('Here are the randomly-generated moves:')
    # Set up the random number generator
    seed(the_seed)
    # Randomise the order in which competitors move
    competitors = ['Competitor A', 'Competitor B', 'Competitor C', 'Competitor D',]
    shuffle(competitors)
    # Decide how many rounds of moves to make
    num_rounds = randint(0, max_rounds)
    # For each round generate a random move for each competitor
    # and save and print it
    moves = []
    for round_no in range(num_rounds):
        print()
        for competitor in competitors:
            # Create a random move
            move = [competitor, choice(['Left', 'Right', 'Up', 'Down'])]
            # Print it to the shell and remember it
            print(move)
            moves.append(move)
    # Print a final message and return the list of moves
    print('\nThere were', len(competitors) * num_rounds,
          'moves generated in', num_rounds,
          ('round' if num_rounds == 1 else 'rounds'))
    return moves

#
#--------------------------------------------------------------------#



#-----Student's Solution---------------------------------------------#
#
#  Complete the assignment by replacing the dummy function below with
#  your own "process_moves" function.
#


# Spy vs Spy Theme

def process_moves(move_list):
    
    # Defining Competitors
    def draw_competitor_A():
        # Black Spy (front on) 
        # Variables for Drawings 
        s = 1 # scale factor
        # Positions
        Right = 0
        Left = 180
        Up = 90
        Down = 270
        #Drawing variables 
        Spy_colour = 'black'
        pen_color = 'black'
        width(2)
        
        # Background colour variable choice 
        background_colour = 'white'
        
        # Middle Position
        middle_pos = xcor(), ycor()

        # Background colour
        setheading(Down)
        pendown()
        forward(45)
        fillcolor(background_colour)
        pencolor('grey')
        begin_fill()
        setheading(Left)
        forward(60)
        setheading(Up)
        forward(90)
        setheading(Right)
        forward(120)
        setheading(Down)
        forward(90)
        setheading(Left)
        forward(60)
        penup()
        end_fill()
        goto(middle_pos)
        
        # Background Dot
        color('pink')
        dot(80)
        
        # Hat Brim
        width(2)
        pendown()
        pencolor('white')
        setheading(Left)
        forward(25*s)
        setheading(Right)
        fillcolor(Spy_colour)
        begin_fill()
        pendown()
        forward(55*s)
        left(145)
        forward(35*s)
        left(70)
        forward(35*s)   
        end_fill() 

        # Setting up Pos
        penup()
        setheading(Right)
        forward(20.75 *s)
        setheading(Up)
        forward(32.5*s)
        
        # Hat Black
        pendown()
        fillcolor(Spy_colour)
        begin_fill()
        setheading(Right)
        forward(15*s) 
        setheading(Down)
        forward(18*s)
        setheading(Left)
        forward(15*s)
        setheading(Up)
        forward(18*s)
        end_fill()

        # Hat White 
        setheading(Down)
        forward(12*s)
        fillcolor('white')
        begin_fill()
        setheading(Right)
        forward(15*s)
        setheading(Down)
        forward(6*s)
        setheading(Left)
        forward(15*s)
        setheading(Up)
        forward(6*s)
        end_fill()
        penup()

        # Body
        setheading(Down)
        forward(20.5*s)
        setheading(Right)
        forward(7*s)
        pendown()
        fillcolor(Spy_colour)
        begin_fill()
        right(70)
        forward(45*s)
        right(110)
        forward(35*s)
        right(110)
        forward(45*s)
        end_fill()
        penup()

        # Face
        pencolor('#F0BB84')
        fillcolor('#F0BB84')
        setheading(Left)
        forward(5*s)
        begin_fill() 
        pendown()
        setheading(Down)
        left(15)
        forward(30*s)
        left(150)
        forward(30*s)
        end_fill()
        # Eyes
        color('black')
        setheading(Down)
        penup()
        forward(4*s)
        setheading(Left)
        forward(5*s)
        pendown()
        dot(5*s)
        penup()
        fillcolor('#F0BB84')
        begin_fill()
        forward(3*s)
        setheading(Up)
        forward(3*s)
        setheading(Right)
        forward(6*s)
        setheading(Down)
        forward(3*s)
        setheading(Left)
        forward(2*s)
        end_fill()
        forward(6*s)
        pendown()
        dot(5*s)
        penup()
        begin_fill()
        forward(3*s)
        setheading(Up)
        forward(3*s)
        setheading(Right)
        forward(6*s)
        setheading(Down)
        forward(3*s)
        setheading(Left)
        forward(2*s)
        end_fill()
        goto(middle_pos)
        
    def draw_competitor_B():
        # White Spy Profile 
        # Variables for Drawings 
        s = 1 # scale factor
        # Positions
        Right = 0
        Left = 180
        Up = 90
        Down = 270
        #Drawing variables 
        Spy_colour = 'white'
        pen_color = 'black'
        width(2)
        
        # Background colour 
        background_colour = 'black'

        # Middle Position
        middle_pos = xcor(), ycor()

        # Background colour
        setheading(Down)
        pendown()
        forward(45*s)
        color(background_colour)
        pencolor('grey')
        begin_fill()
        setheading(Left)
        forward(60*s)
        setheading(Up)
        forward(90*s)
        setheading(Right)
        forward(120*s)
        setheading(Down)
        forward(90*s)
        setheading(Left)
        forward(60*s)
        penup()
        end_fill()
        goto(middle_pos)
        
        # Background Dot
        color('pink')
        dot(80*s)

        #Hat Brim
        width(2)
        setheading(Left)
        forward(24*s)
        fillcolor('white')
        pencolor('black')
        hat_start = xcor(), ycor()
        setheading(Left)
        forward(1*s)
        body_start = xcor(), ycor()
        goto(hat_start) 
        begin_fill()
        right(120)
        pendown()
        forward(20*s)
        hat_top_start = xcor(), ycor() 
        right(30)
        forward(20*s)
        right(20)
        forward(25*s)
        goto(hat_start)
        end_fill()

        #Hat top
        goto(hat_top_start)
        left(110)
        begin_fill()
        forward(18*s)
        right(90)
        forward(20*s)
        right(90)
        forward(18*s)
        end_fill()
        fillcolor('black')
        penup()

        #Hat Black
        goto(hat_top_start)
        pendown()
        right(180)
        begin_fill() 
        forward(8*s)
        right(90)
        forward(20*s)
        right(90)
        forward(8*s)
        right(90)
        forward(20*s)
        end_fill()

        # Head
        penup()
        left(90)
        forward(10*s)
        head_start = xcor(), ycor()
        left(5)
        fillcolor('#F0BB84')
        begin_fill()
        pendown()
        forward(10*s)
        mouth_start = xcor(), ycor()
        forward(30*s) 
        left(150)
        forward(45*s)
        goto(head_start)
        end_fill()


        #Body
        pencolor('black')
        fillcolor('white')
        penup()
        goto(body_start)
        setheading(Down)
        forward(40*s)
        begin_fill()
        body_bit =  xcor(), ycor()
        pendown()
        setheading(Right)
        forward(40*s)
        goto(head_start)
        goto(body_bit)
        end_fill()

        # Eye
        goto(head_start)
        setheading(Right)
        penup()
        forward(15*s)
        setheading(Up)
        forward(5/3*s)
        color('black')
        pendown()
        begin_fill()
        seth(-140)
        circle(16/3*s,90)
        circle(8/3*s,90)
        circle(16/3*s,90)
        small_eye =  xcor(), ycor()
        circle(8/3*s,90)
        end_fill()
        
        #Small Eye 
        penup()
        color('white') 
        goto(small_eye)
        setheading(Left)
        forward(1/3*s)
        setheading(Down)
        forward(1/3*s) 
        pendown() 
        begin_fill()
        seth(-140)
        circle(4/3*s,90)
        circle(2/3*s,90)
        circle(4/3*s,90)
        circle(2/3*s,90)
        end_fill()

        #Mouth
        penup()
        goto(mouth_start)
        setheading(Up)
        color('black')
        pendown() 
        circle(-30/3*s, 50)
        right(90)
        forward(5/3*s)
        right(180)
        forward(10/3*s)
        penup()
        goto(middle_pos)
        
    def draw_competitor_C():
        # Grey Spy (lady) 
        # Variables for Drawings 
        s = 1 # scale factor
        # Positions
        Right = 0
        Left = 180
        Up = 90
        Down = 270
        #Drawing variables 
        pen_color = 'black'
        width(2)
        # Middle Position
        middle_pos = xcor(), ycor()

        # Background colour
        setheading(Down)
        pendown()
        forward(45*s)
        color('gold')
        pencolor('grey') 
        begin_fill()
        setheading(Left)
        forward(60*s)
        setheading(Up)
        forward(90*s)
        setheading(Right)
        forward(120*s)
        setheading(Down)
        forward(90*s)
        setheading(Left)
        forward(60*s)
        penup()
        end_fill()
        goto(middle_pos)
        
        # Background Dot
        color('pink')
        dot(80*s)

        # Hat
        color('#D4D4D4')
        pencolor('black')
        width(3) 
        setheading(Up)
        forward(16*s)
        pendown()
        hat_start = xcor(), ycor()
        begin_fill() 
        setheading(Left)
        forward(30*s)
        right(140)
        forward(20*s)
        left(20)
        forward(10*s)
        setheading(Right) 
        forward(15*s)
        right(80)
        forward(10*s)
        left(50)
        forward(20*s)
        goto(hat_start)
        end_fill() 

        # Face
        setheading(Right)
        fillcolor('#EED2BA')
        forward(10*s)
        right_curl = xcor(), ycor()
        setheading(Down)
        begin_fill()
        forward(5*s)
        circle(-14*s, 100)
        body_start =  xcor(), ycor()
        circle(-14*s, 100)
        end_fill()
        left_curl = xcor(), ycor()

        # Body
        penup()
        goto(body_start)
        setheading(Down)
        fillcolor('#D4D4D4') 
        pendown()
        begin_fill() 
        right(40)
        forward(15*s)
        left(90)
        forward(10*s)
        right(90)
        forward(20*s)
        setheading(Right)
        forward(35*s)
        right(230)
        forward(20*s)
        right(90)
        forward(10*s)
        left(90)
        forward(15*s)
        setheading(Left)
        forward(5*s) 
        end_fill()

        # Right Curl
        width(2) 
        penup()
        fillcolor('#FEF359')  
        goto(right_curl)
        begin_fill() 
        setheading(Down)
        pendown()
        circle(5*s, 100)
        setheading(Down)
        circle(-8*s, 200)
        goto(right_curl)
        end_fill() 

        # Left Curl
        penup()
        goto(left_curl)
        pendown()
        begin_fill()
        setheading(Down)
        right(20)
        circle(15*s, 80)
        setheading(Left)
        forward(3*s) 
        circle(-13*s, 80)
        goto(left_curl)
        end_fill()

        # Eyes
        penup()
        goto(hat_start)
        setheading(Left)
        forward(7*s) 
        setheading(Down)
        forward(6*s)
        middle_eyes = xcor(), ycor()
        pendown()
        fillcolor('white') 
        begin_fill() 
        setheading(Left)
        left(30)
        forward(5*s) 
        right(90)
        forward(4*s)
        right(30)
        forward(4*s)
        goto(middle_eyes)
        end_fill()
        begin_fill() 
        setheading(Right)
        right(30)
        forward(5*s) 
        left(90)
        forward(4*s)
        left(30)
        forward(4*s)
        goto(middle_eyes)
        end_fill()

        # Mouth
        penup()
        setheading(Down)
        forward(6*s)
        setheading(Right)
        begin_fill()
        mouth_start = xcor(), ycor()
        pendown()
        circle(6*s,40)
        setheading(Down)
        circle(-4*s,160)
        goto(mouth_start)
        end_fill()
        penup()
        goto(middle_pos)
 
        
        
    def draw_competitor_D():
        # White Leader 
        # Variables for Drawings 
        s = 1 # scale factor
        # Positions
        Right = 0
        Left = 180
        Up = 90
        Down = 270
        #Drawing variables 
        pen_color = 'black'
        width(2)
        # Middle Position
        middle_pos = xcor(), ycor()

        # Background colour
        setheading(Down)
        pendown()
        forward(45*s)
        color('dark blue')
        pencolor('grey') 
        begin_fill()
        setheading(Left)
        forward(60*s)
        setheading(Up)
        forward(90*s)
        setheading(Right)
        forward(120*s)
        setheading(Down)
        forward(90*s)
        setheading(Left)
        forward(60*s)
        penup()
        end_fill()
        goto(middle_pos)
        
        # Background Dot
        color('pink')
        dot(80*s)

        # Head
        width(3)
        color('black')
        fillcolor('white')
        setheading(Up)
        forward(20*s)
        setheading(Left) 
        forward(10*s)
        setheading(Up)
        left(30)  
        pendown()
        begin_fill()
        hat_start = xcor(), ycor()
        circle(-10*s, 100)
        circle(-5*s, 130)
        left(30)
        circle(15*s, 50)
        right(40)
        circle(-5*s, 60)
        face_end = xcor(), ycor()
        goto(hat_start)
        end_fill() 
        

        # Face
        fillcolor('#F0BB84')
        begin_fill()
        setheading(Down)
        forward(20*s)
        body_start = xcor(), ycor()
        setheading(Right)
        forward(20*s)
        goto(face_end)
        goto(hat_start) 
        end_fill()

        # Star
        penup()
        setheading(Right)
        forward(9*s) 
        color('gold') 
        dot(5*s)

        # Body
        goto(body_start)
        fillcolor('white') 
        pencolor('black')
        setheading(Left)
        pendown()
        begin_fill()
        forward(10*s)
        setheading(Down)
        forward(35*s)
        setheading(Right)
        forward(40*s)
        setheading(Up)
        forward(35*s)
        setheading(Left)
        forward(20*s)
        middle = xcor(), ycor()
        forward(20*s) 
        end_fill()
        goto(middle)
        setheading(Down)
        forward(10*s)
        button_one = xcor(), ycor()
        forward(10*s)
        button_two = xcor(), ycor()
        forward(15*s)
        penup()

        # Buttons
        goto(button_one)
        setheading(Left)
        forward(10*s)
        color('black')
        dot(5*s)
        setheading(Right)
        forward(20*s)
        dot(5*s)
        goto(button_two)
        setheading(Left)
        forward(10*s)
        color('black')
        dot(5*s)
        setheading(Right)
        forward(20*s)
        dot(5*s)

        # Eyes
        goto(middle_pos)
        pencolor('black')
        setheading(Up)
        forward(14*s)
        setheading(Left)
        forward(4*s)
        pendown()
        begin_fill()
        seth(-140)
        circle(2*s,90)
        circle(1*s,90)
        circle(2*s,90)
        circle(1*s,90)
        end_fill() 
        setheading(Right)
        penup()
        forward(4*s)
        pendown()
        begin_fill()
        seth(-140)
        circle(2*s,90)
        circle(1*s,90)
        circle(2*s,90)
        circle(1*s,90)
        end_fill()
        penup()

        # Mouth
        setheading(Down)
        forward(8*s)
        pendown()
        setheading(Left)
        forward(6*s)
        setheading(Right)
        forward(12*s)
        penup()
        goto(middle_pos) 
        
    # Placing competitors in starting positions
    Position_A = (-120*3, 90*3)
    Position_B = (120*3, 90*3)
    Position_C = (-120*3, -90*3)
    Position_D = (120*3, -90*3)

    # Angles
    Right = 0
    Left = 180
    Up = 90
    Down = 270
    # Middle Position
    middle_pos = xcor(), ycor()

    # Competitor A (Black Spy)
    goto(Position_A)
    draw_competitor_A()
    # Writting Competitor A
    setheading(Left)
    forward(300)
    color('black')
    write("Competitor A (Black Spy)", font=("Arial", 12, "bold"))

    # Competitor B (White Spy)
    goto(Position_B)
    draw_competitor_B()
    # Writting Competitor B
    setheading(Left)
    forward(-100)
    color('black')
    write("Competitor B (White Spy)", font=("Arial", 12, "bold"))

    # Competitor C (Grey Spy)    
    goto(Position_C)
    draw_competitor_C()
    # Writting Competitor C
    setheading(Left)
    forward(300)
    color('black')
    write("Competitor C (Grey Spy)", font=("Arial", 12, "bold"))

    # Competitor D (White Leader) 
    goto(Position_D)
    draw_competitor_D()
    # Writting Competitor D
    setheading(Left)
    forward(-100)
    color('black')
    write("Competitor D (White Leader)", font=("Arial", 12, "bold"))


    # Define the moves 
    def move_right():
        setheading(0)
        forward(120)
        
    def move_left():
        setheading(180)
        forward(120)
        
    def move_up():
        setheading(90)
        forward(90)
        
    def move_down():
        setheading(270)
        forward(90)
        
    # Define the move check
    def move_go():
        if move[1] == "Right":
            move_right()
        elif move[1] == "Left":
            move_left()
        elif move[1] == "Up":
            move_up()     
        elif move[1] == "Down":
            move_down()
            
    # Part B Additional list variable    
    winner = []

    # Move the Competitor and draw Token
    for move in move_list:
            if move[0] == 'Competitor A':
                goto(Position_A)
                move_go()
                 # Check in grid
                if xcor() < 420 and xcor() > -420 and ycor() < 315 and ycor() > -315:
                    draw_competitor_A()
                    Position_A = (xcor(), ycor())
                    # Part B (checking middle)
                    # Check in middle
                    if xcor() < 60 and xcor() > -60 and ycor() < 45 and ycor() > -45:
                        winner.append('Competitor A')
                else:
                    pass
            elif move[0] == 'Competitor B':
                goto(Position_B)
                move_go()
                # Check in grid
                if xcor() < 420 and xcor() > -420 and ycor() < 315 and ycor() > -315:
                    draw_competitor_B()
                    Position_B = (xcor(), ycor())
                    # Part B (checking middle)
                    # Check in middle
                    if xcor() < 60 and xcor() > -60 and ycor() < 45 and ycor() > -45:
                        winner.append('Competitor B')
                else:
                    pass
            elif move[0] == 'Competitor C':
                goto(Position_C)
                move_go()
                # Check in grid
                if xcor() < 420 and xcor() > -420 and ycor() < 315 and ycor() > -315:
                    draw_competitor_C()
                    Position_C = (xcor(), ycor())
                    # Part B (checking middle)
                    # Check in middle
                    if xcor() < 60 and xcor() > -60 and ycor() < 45 and ycor() > -45: 
                        winner.append('Competitor C')
                else:
                    pass
            elif move[0] == 'Competitor D':
                goto(Position_D)
                move_go()
                # Check in grid
                if xcor() < 420 and xcor() > -420 and ycor() < 315 and ycor() > -315:
                    draw_competitor_D()
                    Position_D = (xcor(), ycor())
                    # Part B (checking middle)
                    # Check in middle
                    if xcor() < 60 and xcor() > -60 and ycor() < 45 and ycor() > -45:
                        winner.append('Competitor D')
                else:
                    pass
             

    # Part B Reaching Home
    penup()
    goto((grid_size * cell_width) // 2 + 50, -cell_height // 4)          
    pendown()
    color('black')
    if len(winner) == 0:
        # No one reached home
        write('No competitor\nreached the\nhome square' , font=("Arial", 22, "bold"))
    else:
        # Someone reached home
        write('First competitor\nto reach home:\n '+ winner[0] , font=("Arial", 22, "bold"))
        penup()
        setheading(Down)
        forward(60)
        setheading(Right)
        forward(100)
        if winner[0] == 'Competitor A':
            draw_competitor_A()
        elif winner[0] == 'Competitor B':
            draw_competitor_B()
        elif winner[0] == 'Competitor C':
            draw_competitor_C()
        elif winner[0] == 'Competitor D':
            draw_competitor_D() 
    

#
#--------------------------------------------------------------------#



#-----Main Program---------------------------------------------------#
#
# This main program sets up the canvas, ready for you to start
# drawing your solution, and calls your solution.  Do not change
# any of this code except as indicated by the comments marked '*****'.
#

# Set up the drawing canvas
# ***** You can change the background and line colours, choose
# ***** whether or not to label the axes, etc, by providing
# ***** arguments to this function call
create_drawing_canvas(False)

# Control the drawing speed
# ***** Change the following argument if you want to adjust
# ***** the drawing speed
speed('fastest')

# Decide whether or not to show the drawing step-by-step
# ***** Set the following argument to False if you don't want to wait
# ***** forever while the cursor moves slowly around the screen
tracer(False)

# Give the drawing canvas a title
# ***** Replace this title with a description of your solution's
# ***** theme and its competitors
title('Spy Network')

### Call the student's function to process the moves
### ***** While developing your program you can call the
### ***** "random_moves" function with a fixed seed for the random
### ***** number generator, but your final solution must work with
### ***** "random_moves()" as the argument to "process_moves", i.e.,
### ***** for any data set that can be returned by calling
### ***** "random_moves" with no seed.
process_moves(random_moves()) # <-- this will be used for assessment

### Alternative function call to help debug your code
### ***** The following function call can be used instead of
### ***** the one above while debugging your code, but will
### ***** not be used for assessment. Comment out the call
### ***** above and uncomment the one below if you want to
### ***** call your "process_moves" function with one of the
### ***** "fixed" data sets above, so that you know in advance
### ***** what the moves are.
# process_moves(fixed_data_set_00) # <-- not used for assessment

# Exit gracefully
# ***** Change the default argument to False if you want the
# ***** cursor (turtle) to remain visible at the end of the
# ***** program as a debugging aid.
release_drawing_canvas(True)

#
#--------------------------------------------------------------------#
