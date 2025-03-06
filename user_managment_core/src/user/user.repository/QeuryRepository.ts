import { Injectable, NotFoundException } from '@nestjs/common';
import { JwtService } from '@nestjs/jwt';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import * as bcrypt from 'bcryptjs';
import { User } from 'src/user/entities/registracion.entity';
import { IuserQeuryRepository } from '../libs/contracts/user.repository.Interface';


@Injectable()
export class UserQeuryRepository implements IuserQeuryRepository {
  constructor(
    @InjectRepository(User) private readonly userRepository: Repository<User>,private jwtService: JwtService)
   { }

  findAll(): Promise<any> {
    throw new Error('Method not implemented.');
  }

  
  async validateUser(email: string, password: string): Promise<any> {
    
   console.log("password",password)
   let passwordValid=await bcrypt.compare(password, password);
   
      return passwordValid;
    

  }


//*************************************************************************

  
  async getProfileById(id: string): Promise<any> {
    
    const user = await this.userRepository.findOne({ where: { id: id } });
    if (!user) {
        throw new NotFoundException('User not found');
    }

    return user;
}

  
//*************************************************************************


  
async getProfileByEmail(useremail: string): Promise<any> {
    
  const user = await this.userRepository.findOne({ where: {  email:useremail } });
  if (!user) {
      throw new NotFoundException('User not found');
  }

  return user;
}


}



