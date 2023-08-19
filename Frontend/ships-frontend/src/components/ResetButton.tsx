import React from 'react'

export default function ResetButton({ handleClick }: any) {
  return (
    <button className='reset-button' onClick={handleClick}>
      Reset Game
    </button>)
}
