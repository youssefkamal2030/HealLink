
import 'package:flutter/cupertino.dart';
import 'package:heal_link/core/widgets/custom_otp_input_row.dart';

class DoctorVerifyEmailOtpRow extends StatelessWidget {
  const DoctorVerifyEmailOtpRow({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(child: CustomOtpInputRow());
  }
}