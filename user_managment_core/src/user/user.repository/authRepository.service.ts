import { Injectable, NotFoundException } from '@nestjs/common';
import { JwtService } from '@nestjs/jwt';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import * as bcrypt from 'bcryptjs';
import { User } from 'src/user/entities/registracion.entity';
import { Userrepositoryinterface } from './contracts/user.repository.Interface';


@Injectable()
export class AuthRepository   implements Partial<Userrepositoryinterface>{
  constructor(
    @InjectRepository(User) private readonly userRepository: Repository<User>,
    private jwtService: JwtService,
  )
   { }

  
  async validateUser(email: string, password: string): Promise<any> {
    
    const user = await this.userRepository.findOne({ where: { email } });
    
    if (user && (await bcrypt.compare(password, user.passWord))) {
      const { passWord, ...result } = user;
     
      return result;
    }
    return null;
  }
  //*************************************************************************/

  async login(user: any) {
    const payload = { username: user.email, sub: user.id };
    return {
      access_token: this.jwtService.sign(payload),
    };
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



