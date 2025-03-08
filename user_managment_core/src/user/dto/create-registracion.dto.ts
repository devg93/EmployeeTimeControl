import { ApiProperty } from '@nestjs/swagger';
import { IsEmail, IsNotEmpty, MinLength, IsIP, IsOptional } from 'class-validator';

export class CreateRegistracionDto {
  @ApiProperty({ example: 'JohnDoe', description: 'User name' })
  @IsNotEmpty()
  userName: string;

  @ApiProperty({ example: 'johndoe@example.com', description: 'User email' })
  @IsEmail()
  email: string;

  @ApiProperty({ example: 'SecurePassword123', description: 'User password' })
  @IsNotEmpty()
  @MinLength(6)
  password: string;

  @ApiProperty({ example: '192.168.1.1', description: 'User IP address', required: false })
  @IsOptional() 
  @IsIP()
  iPadrres?: string;


  @ApiProperty({ example: 'phone', description: 'User device ', required: false })
  @IsOptional() 
  @IsIP()
  deviceName?: string;
}
