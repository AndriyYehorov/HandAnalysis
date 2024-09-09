import { Button } from '@chakra-ui/react'

export default function PlayCard({rank, suit, onCardSelect}) {

    const handleClick = () =>
    {
      onCardSelect({rank, suit});
    };


    return (
      <>  
        <Button className='flex justify-center items-center' colorScheme='teal' variant='solid' onClick={handleClick}>                    
            {rank}{suit}                   
        </Button>
      </>
    )
}

        